using System;
using System.Runtime.Serialization;

namespace Model
{
    public class Department : ISerializable
    {
        public short DepartmentID { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        
        
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("DepartmentID", DepartmentID, typeof(short));
            info.AddValue("Name", Name, typeof(string));
            info.AddValue("GroupName", GroupName, typeof(string));
            info.AddValue("ModifiedDate", ModifiedDate, typeof(System.DateTime));
        }

        public Department(short departmentId, string name, string groupName, DateTime modifiedDate)
        {
            DepartmentID = departmentId;
            Name = name;
            GroupName = groupName;
            ModifiedDate = modifiedDate;
        }

        public Department()
        {
        }

        protected bool Equals(Department other)
        {
            return DepartmentID == other.DepartmentID && Name == other.Name && GroupName == other.GroupName && ModifiedDate.Equals(other.ModifiedDate);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Department) obj);
        }

    }
}