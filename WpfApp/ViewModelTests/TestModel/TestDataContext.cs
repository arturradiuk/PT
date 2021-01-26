using System.Collections.Generic;
using System.Runtime.Serialization;
using Logic;
using Model;

namespace ViewModelTests.TestModel
{
    public class TestDataContext : IDataContext
    {
        private IDataService _service;

        public TestDataContext(IDataService _service)
        {
            this._service = _service;
        }

        public TestDataContext()
        {
            this._service = new DataService();
        }

        public Department GetDepartmentFromISerializable(ISerializable iSerializable)
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
        
        public List<IDepartment> GetAllDepartments()
        {
            IEnumerable<ISerializable> tempDeps = this._service.GetAllDepartments();
            List<IDepartment> departments = new List<IDepartment>();

            foreach (ISerializable var in tempDeps)
            {
                Department department = this.GetDepartmentFromISerializable(var);
                departments.Add(department);
            }
            return departments;
        }

        public void RemoveDepartment(short departmentID)
        {
            this._service.RemoveDepartment(departmentID);
        }

        public void UpdateDepartment(short departmentID, IDepartment department)
        {
            this._service.UpdateDepartment(departmentID, department);
        }

        public void AddDepartment(ISerializable department)
        {
            this._service.AddDepartment(department);
        }
    }
}