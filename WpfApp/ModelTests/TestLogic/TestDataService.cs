using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Model;
using ModelTests.TestData;

namespace ModelTests.TestLogic
{
    public class TestDataService : IDisposable, ITestDataService
    {
        private readonly TestLocalDataContext _tdc;

        public TestDataService(TestLocalDataContext ldc)
        {
            _tdc = ldc;
        }

        public TestDataService()
        {
            _tdc = new TestLocalDataContext();
        }

        public void Dispose()
        {
            _tdc.Departments.Clear();
        }


        public IEnumerable<ISerializable> GetAllDepartments()
        {
            return _tdc.Departments;
        }

        public void AddDepartment(ISerializable department)
        {
            ObservableCollection<IDepartment> departments = _tdc.Departments;
            IDepartment department_temp = GetDepartmentFromISerializable(department);
            _tdc.Departments.Add(department_temp);
        }

        public void RemoveDepartment(short departmentID)
        {
            try
            {
                IDepartment department = (from d in _tdc.Departments
                    where d.DepartmentID == departmentID
                    select d).First();
                _tdc.Departments.Remove(department);
            }
            catch (Exception e)
            {
                throw new KeyNotFoundException("No department with this ID");
            }
        }

        public ISerializable GetDepartmentById(short departmentID)
        {
            return _tdc.Departments.First(department => department.DepartmentID.Equals(departmentID));
        }

        public void UpdateDepartment(short departmentID, ISerializable department)
        {
            IDepartment department_temp = GetDepartmentFromISerializable(department);

            Department dbDepartment = GetDepartmentById(departmentID) as Department;

            foreach (PropertyInfo property in dbDepartment.GetType().GetProperties())
                property.SetValue(dbDepartment, property.GetValue(department_temp));

            dbDepartment.DepartmentID = departmentID;
        }

        public IDepartment GetDepartmentFromISerializable(ISerializable iSerializable)
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