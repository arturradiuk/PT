using System.Collections.Generic;
using System.Runtime.Serialization;
using Logic;
using Model;

namespace ViewModelTests.TestModel
{
    public class TestDataContext : IDataContext
    {
        private readonly IDataService _service;

        public TestDataContext(IDataService _service)
        {
            this._service = _service;
        }

        public TestDataContext()
        {
            _service = new DataService();
        }

        public List<IDepartment> GetAllDepartments()
        {
            IEnumerable<ISerializable> tempDeps = _service.GetAllDepartments();
            List<IDepartment> departments = new List<IDepartment>();

            foreach (ISerializable var in tempDeps)
            {
                Department department = GetDepartmentFromISerializable(var);
                departments.Add(department);
            }

            return departments;
        }

        public void RemoveDepartment(short departmentID)
        {
            _service.RemoveDepartment(departmentID);
        }

        public void UpdateDepartment(short departmentID, IDepartment department)
        {
            _service.UpdateDepartment(departmentID, department);
        }

        public void AddDepartment(ISerializable department)
        {
            _service.AddDepartment(department);
        }

        public Department GetDepartmentFromISerializable(ISerializable iSerializable)
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