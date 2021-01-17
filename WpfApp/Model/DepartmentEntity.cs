using System.Runtime.Serialization;
using CustomData;
using Data;

namespace Model
{
    public class DepartmentEntity : Department, ISerializable
    {
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}