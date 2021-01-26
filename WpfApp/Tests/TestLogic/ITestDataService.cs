using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ViewModelTest.TestLogic
{
    public interface ITestDataService
    {
        IEnumerable<ISerializable> GetAllDepartments();

        void RemoveDepartment(short departmentID);

        void UpdateDepartment(short departmentID, ISerializable department);
        void AddDepartment(ISerializable department);

    }
}