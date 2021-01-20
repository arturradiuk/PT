using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Text;
using Data;

namespace Logic
{
    public class DataService : IDisposable, IDataService
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }


        private readonly LocalDataContext _ldc;

        public DataService(LocalDataContext ldc)
        {
            this._ldc = ldc;
        }

        public DataService()
        {
            this._ldc = new LocalDataContext();
        }


        public IEnumerable<ISerializable> GetAllDepartments()
        {
            return _ldc.GetTable<Department>().ToList();
        }

        public void AddDepartment(ISerializable department)
        {
            Table<Department> departments = _ldc.GetTable<Department>();//
            Department department_temp = GetDepartmentFromISerializable(department);
            
            // Department department_temp = new Department();
            // department_temp.Name = "aaa";
            // department_temp.GroupName = "aaa";
            // department_temp.DepartmentID = 0;
            // department_temp.ModifiedDate = DateTime.Now;

            // department_temp2.Name = department_temp.Name;
            // department_temp2.GroupName = department_temp.GroupName;
            // department_temp2.DepartmentID = department_temp.DepartmentID;
            // department_temp2.ModifiedDate = department_temp.ModifiedDate;

    
            departments.InsertOnSubmit(department_temp);

            // Department department_temp = new Department();
            // department_temp.Name = "dsfd" + new Random().Next(50004);
            // department_temp.GroupName = "fd";
            // department_temp.ModifiedDate = DateTime.Now;
            // departments.InsertOnSubmit(department_temp);
            
            this._ldc.SubmitChanges();
        }

        public void RemoveDepartment(short departmentID)
        {
            Table<EmployeeDepartmentHistory> edh = _ldc.GetTable<EmployeeDepartmentHistory>();
            IEnumerable<EmployeeDepartmentHistory> edhEnumerable = (from e in edh
                where e.DepartmentID == departmentID
                select e);
            edh.DeleteAllOnSubmit(edh);

            Table<Department> departments = _ldc.GetTable<Department>();
            Department tempDep = this.GetDepartmentById(departmentID) as Department;
            departments.DeleteOnSubmit(tempDep);

            this._ldc.SubmitChanges();
        }

        public ISerializable GetDepartmentById(short departmentID)
        {
            Table<Department> departments = _ldc.GetTable<Department>();
            return departments.First(department => department.DepartmentID.Equals(departmentID));
        }

        public void UpdateDepartment(short departmentID, ISerializable department)
        {
            Department department_temp = GetDepartmentFromISerializable(department);
            
            Table<Department> departments = _ldc.GetTable<Department>();
            Department dbDepartment = GetDepartmentById(departmentID) as Department;

            foreach (var property in dbDepartment.GetType().GetProperties())
            {
                property.SetValue(dbDepartment, property.GetValue(department_temp));
            }

            dbDepartment.DepartmentID = departmentID;
            this._ldc.SubmitChanges();
        }
        
        private Department GetDepartmentFromISerializable(ISerializable iSerializable)
        {
            Department department = new Department();
            SerializationInfo si = new SerializationInfo(iSerializable.GetType(),new FormatterConverter());
            iSerializable.GetObjectData(si,new StreamingContext());
            department.Name=si.GetString("Name");
            department.DepartmentID=si.GetInt16("DepartmentID");
            department.GroupName=si.GetString("GroupName");
            department.ModifiedDate=si.GetDateTime("ModifiedDate");
            return department;
        }
    }
}