using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Model
{
    public interface IDataContext
    {
        void AddDepartment(ISerializable department);
        List<IDepartment> GetAllDepartments();
        void RemoveDepartment(short departmentID);
        void UpdateDepartment(short departmentID, IDepartment department);
    }
}