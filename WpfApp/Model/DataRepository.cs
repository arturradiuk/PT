using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Model
{
    public class DataRepository : IDisposable
    {
        private readonly LocalDataContext _ldc;

        public DataRepository(LocalDataContext ldc)
        {
            this._ldc = ldc;
        }

        public DataRepository()
        {
            this._ldc = new LocalDataContext();
        }


        public IEnumerable<Department> GetAllDepartments()
        {
            return _ldc.GetTable<Department>();
        }

        public void AddDepartment(Department department)
        {
            Table<Department> departments = _ldc.GetTable<Department>();
            departments.InsertOnSubmit(department);
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
            Department tempDep = this.GetDepartmentByID(departmentID, departments);
            departments.DeleteOnSubmit(tempDep);

            this._ldc.SubmitChanges();
        }

        public Department GetDepartmentByID(short departmentID, Table<Department> departments)
        {
            return departments.First(department => department.DepartmentID.Equals(departmentID));
        }

        public void UpdateDepartment(short departmentID, Department department)
        {
            Table<Department> departments = _ldc.GetTable<Department>();
            Department dbDepartment = GetDepartmentByID(departmentID, departments);

            foreach (var property in dbDepartment.GetType().GetProperties())
            {
                property.SetValue(dbDepartment, property.GetValue(department));
            }

            dbDepartment.DepartmentID = departmentID;
            this._ldc.SubmitChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}