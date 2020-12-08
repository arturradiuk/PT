using System.Runtime.Serialization;

namespace ConsoleApp
{
    [DataContract]
    public class ClassC
    {
        [DataMember] public string StringProperty { get; set; }
        [DataMember] public float FloatProperty { get; set; }
        [DataMember] public int IntProperty { get; set; }
        [DataMember] public bool BoolProperty { get; set; }
        [DataMember] public ClassA AProperty { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("StringProperty", StringProperty, typeof(string));
            info.AddValue("FloatProperty", FloatProperty, typeof(float));
            info.AddValue("IntProperty", IntProperty, typeof(int));
            info.AddValue("BoolProperty", BoolProperty, typeof(bool));
            info.AddValue("AProperty", AProperty, typeof(ClassA));
        }

        public override string ToString()
        {
            return
                $"{StringProperty}=StringProperty,{FloatProperty}=FloatProperty,{IntProperty}=IntProperty,{BoolProperty}=BoolProperty";
        }

        public ClassC(string stringProperty, float floatProperty, int intProperty, bool boolProperty, ClassA aProperty)
        {
            StringProperty = stringProperty;
            FloatProperty = floatProperty;
            IntProperty = intProperty;
            BoolProperty = boolProperty;
            AProperty = aProperty;
        }

        public ClassC()
        {
        }

        public override bool Equals(object obj)
        {
            ClassC inst = (ClassC) obj;
            return this.BoolProperty.Equals(inst.BoolProperty) && this.IntProperty.Equals(inst.IntProperty)
                                                               && this.FloatProperty.Equals(inst.FloatProperty)
                                                               && this.StringProperty.Equals(inst.StringProperty);
        }
    }
}