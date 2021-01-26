using System.Collections.Generic;
using System.Runtime.Serialization;
using Logic;
using Model;

namespace ViewModelTests.TestLogic
{
    public interface ITestDataService : IDataService
    {
        IEnumerable<ISerializable> GetAllDepartments();

        void RemoveDepartment(short departmentID);

        void UpdateDepartment(short departmentID, ISerializable department);
        void AddDepartment(ISerializable department);
        IDepartment GetDepartmentFromISerializable(ISerializable iSerializable);
        ISerializable GetDepartmentById(short departmentID);
    }
}