using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Runtime.Serialization;
using ViewModelTest.TestData;

namespace ViewModelTest.TestLogic
{
    public class TestDataService : IDisposable, ITestDataService
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }


        private readonly TestDataContext _tdc;

        public TestDataService(TestDataContext ldc)
        {
            this._tdc = ldc;
        }

        public TestDataService()
        {
            this._tdc = new TestDataContext();
        }


        public IEnumerable<ISerializable> GetAllDepartments()
        {
            return _tdc.GetTable<Department>().ToList();
        }

        public void AddDepartment(ISerializable department)
        {
            Table<Department> departments = _tdc.GetTable<Department>(); //
            Department department_temp = GetDepartmentFromISerializable(department);
            departments.InsertOnSubmit(department_temp);
            try
            {
                this._tdc.SubmitChanges();
            }
            catch (Exception e)
            {
                departments.DeleteOnSubmit(department_temp);
            }
        }

        public void RemoveDepartment(short departmentID)
        {
            Table<EmployeeDepartmentHistory> edh = _tdc.GetTable<EmployeeDepartmentHistory>();
            IEnumerable<EmployeeDepartmentHistory> edhEnumerable = (from e in edh
                where e.DepartmentID == departmentID
                select e);
            
            Table<Department> departments = _tdc.GetTable<Department>();
            Department tempDep = this.GetDepartmentById(departmentID) as Department;
            edh.DeleteAllOnSubmit(edh);
            departments.DeleteOnSubmit(tempDep);
            
            try
            {

                this._tdc.SubmitChanges();

            }
            catch (Exception e)
            {
                edh.InsertAllOnSubmit(edh);
                departments.InsertOnSubmit(tempDep);
            }

        }

        public ISerializable GetDepartmentById(short departmentID)
        {
            Table<Department> departments = _tdc.GetTable<Department>();
            return departments.First(department => department.DepartmentID.Equals(departmentID));
        }

        public void UpdateDepartment(short departmentID, ISerializable department)
        {
            Department department_temp = GetDepartmentFromISerializable(department);

            Table<Department> departments = _tdc.GetTable<Department>();
            Department dbDepartment = GetDepartmentById(departmentID) as Department;

            foreach (var property in dbDepartment.GetType().GetProperties())
            {
                property.SetValue(dbDepartment, property.GetValue(department_temp));
            }

            dbDepartment.DepartmentID = departmentID;
            this._tdc.SubmitChanges();
        }

        private Department GetDepartmentFromISerializable(ISerializable iSerializable)
        {
            Department department = new Department();
            SerializationInfo si = new SerializationInfo(iSerializable.GetType(), new FormatterConverter());
            iSerializable.GetObjectData(si, new StreamingContext());
            department.Name = si.GetString("Name");
            department.DepartmentID = si.GetInt16("DepartmentID");
            department.GroupName = si.GetString("GroupName");
            department.ModifiedDate = si.GetDateTime("ModifiedDate");
            return department;
        }
    }
}