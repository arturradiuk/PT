using System.Collections.Generic;

namespace Model
{
    public interface IDataContext
    {
        List<Department> GetAllDepartments();
        Department GetDepartmentById(short departmentId);
        void RemoveDepartment(short departmentID);
        void UpdateDepartment(short departmentID, Department department);
    }
}