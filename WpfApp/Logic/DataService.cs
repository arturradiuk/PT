using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Data;

namespace Logic
{
    public class DataService : IDisposable, IDataService
    {
        private readonly LocalDataContext _ldc;

        public DataService(LocalDataContext ldc)
        {
            _ldc = ldc;
        }

        public DataService()
        {
            _ldc = new LocalDataContext();
        }


        public IEnumerable<ISerializable> GetAllDepartments()
        {
            return _ldc.GetTable<Department>().ToList();
        }

        public void AddDepartment(ISerializable department)
        {
            var departments = _ldc.GetTable<Department>(); //
            var department_temp = GetDepartmentFromISerializable(department);
            departments.InsertOnSubmit(department_temp);
            try
            {
                _ldc.SubmitChanges();
            }
            catch (Exception e)
            {
                departments.DeleteOnSubmit(department_temp);
            }
        }

        public void RemoveDepartment(short departmentID)
        {
            var edh = _ldc.GetTable<EmployeeDepartmentHistory>();
            IEnumerable<EmployeeDepartmentHistory> edhEnumerable = from e in edh
                where e.DepartmentID == departmentID
                select e;

            var departments = _ldc.GetTable<Department>();
            var tempDep = GetDepartmentById(departmentID) as Department;
            edh.DeleteAllOnSubmit(edh);
            departments.DeleteOnSubmit(tempDep);

            try
            {
                _ldc.SubmitChanges();
            }
            catch (Exception e)
            {
                edh.InsertAllOnSubmit(edh);
                departments.InsertOnSubmit(tempDep);
            }
        }

        public void UpdateDepartment(short departmentID, ISerializable department)
        {
            var department_temp = GetDepartmentFromISerializable(department);

            var departments = _ldc.GetTable<Department>();
            var dbDepartment = GetDepartmentById(departmentID) as Department;

            foreach (var property in dbDepartment.GetType().GetProperties())
                property.SetValue(dbDepartment, property.GetValue(department_temp));

            dbDepartment.DepartmentID = departmentID;
            _ldc.SubmitChanges();
        }

        public void Dispose()
        {
            _ldc.Dispose();
        }

        public ISerializable GetDepartmentById(short departmentID)
        {
            var departments = _ldc.GetTable<Department>();
            return departments.First(department => department.DepartmentID.Equals(departmentID));
        }

        private Department GetDepartmentFromISerializable(ISerializable iSerializable)
        {
            var department = new Department();
            var si = new SerializationInfo(iSerializable.GetType(), new FormatterConverter());
            iSerializable.GetObjectData(si, new StreamingContext());
            department.Name = si.GetString("Name");
            department.DepartmentID = si.GetInt16("DepartmentID");
            department.GroupName = si.GetString("GroupName");
            department.ModifiedDate = si.GetDateTime("ModifiedDate");
            return department;
        }
    }
}