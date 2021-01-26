using System.Runtime.Serialization;

namespace Model
{
    public interface IDepartment : ISerializable
    {
        short DepartmentID { get; set; }
        string Name { get; set; }
        string GroupName { get; set; }
        System.DateTime ModifiedDate { get; set; }
        void GetObjectData(SerializationInfo info, StreamingContext context);
    }
}