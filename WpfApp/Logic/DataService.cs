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
            Table<Department> departments = _ldc.GetTable<Department>();
            departments.InsertOnSubmit(department as Department);
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
            Table<Department> departments = _ldc.GetTable<Department>();
            Department dbDepartment = GetDepartmentById(departmentID) as Department;

            foreach (var property in dbDepartment.GetType().GetProperties())
            {
                property.SetValue(dbDepartment, property.GetValue(department));
            }

            dbDepartment.DepartmentID = departmentID;
            this._ldc.SubmitChanges();
        }
    }
}