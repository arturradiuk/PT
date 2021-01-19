using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Logic;

namespace Model
{
    public class DataContext
    {
        private IDataService _service;

        public DataContext(IDataService _service)
        {
            this._service = _service;
        }

        public DataContext()
        {
            this._service = new DataService();
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
        
        public List<Department> GetAllDepartments()
        {
            IEnumerable<ISerializable> tempDeps = this._service.GetAllDepartments();
            List<Department> departments = new List<Department>();

            foreach (ISerializable var in tempDeps)
            {
                Department department = this.GetDepartmentFromISerializable(var);
                departments.Add(department);
            }
            return departments;
        }        
        
        public Department GetDepartmentById(short departmentId)
        {
            ISerializable serializable = this._service.GetDepartmentById(departmentId);
            return this.GetDepartmentFromISerializable(serializable);
        }
        
        
    }
}