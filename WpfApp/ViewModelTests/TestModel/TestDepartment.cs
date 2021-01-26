using System;
using System.Runtime.Serialization;
using Model;

namespace ViewModelTests.TestModel
{
    public class TestDepartment : IDepartment
    {
        public TestDepartment(short departmentId, string name, string groupName, DateTime modifiedDate)
        {
            DepartmentID = departmentId;
            Name = name;
            GroupName = groupName;
            ModifiedDate = modifiedDate;
        }

        public TestDepartment()
        {
        }

        public short DepartmentID { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public DateTime ModifiedDate { get; set; }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("DepartmentID", DepartmentID, typeof(short));
            info.AddValue("Name", Name, typeof(string));
            info.AddValue("GroupName", GroupName, typeof(string));
            info.AddValue("ModifiedDate", ModifiedDate, typeof(DateTime));
        }

        protected bool Equals(Department other)
        {
            return DepartmentID == other.DepartmentID && Name == other.Name && GroupName == other.GroupName &&
                   ModifiedDate.Equals(other.ModifiedDate);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Department) obj);
        }
    }
}