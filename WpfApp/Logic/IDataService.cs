using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Logic
{
    public interface IDataService
    {
         IEnumerable<ISerializable> GetAllDepartments();
         ISerializable GetDepartmentById(short departmentID);

    }
}