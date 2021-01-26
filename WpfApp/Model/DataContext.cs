﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Logic;

namespace Model
{
    public class DataContext : IDataContext
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
        

        public void RemoveDepartment(short departmentID)
        {
            this._service.RemoveDepartment(departmentID);
        }

        public void UpdateDepartment(short departmentID, Department department)
        {
            this._service.UpdateDepartment(departmentID, department);
        }

        public void AddDepartment(ISerializable department)
        {
            this._service.AddDepartment(department);
        }
    }
}