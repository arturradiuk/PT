using System.Runtime.Serialization;

namespace ConsoleApp
{
    [DataContract]
    public class ClassA
    {
        [DataMember] public string StringProperty { get; set; }
        [DataMember] public float FloatProperty { get; set; }
        [DataMember] public int IntProperty { get; set; }
        [DataMember] public bool BoolProperty { get; set; }
        [DataMember] public ClassB BProperty { get; set; }

        public ClassA(string stringProperty, float floatProperty, int intProperty, bool boolProperty, ClassB bProperty)
        {
            StringProperty = stringProperty;
            FloatProperty = floatProperty;
            IntProperty = intProperty;
            BoolProperty = boolProperty;
            BProperty = bProperty;
        }

        public ClassA()
        {
        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("StringProperty", StringProperty, typeof(string));
            info.AddValue("FloatProperty", FloatProperty, typeof(float));
            info.AddValue("IntProperty", IntProperty, typeof(int));
            info.AddValue("BoolProperty", BoolProperty, typeof(bool));
            info.AddValue("BProperty", BProperty, typeof(ClassB));
        }

        public override string ToString()
        {
            return
                $"{StringProperty}=StringProperty,{FloatProperty}=FloatProperty,{IntProperty}=IntProperty,{BoolProperty}=BoolProperty";
        }

        public override bool Equals(object obj)
        {
            ClassA inst = (ClassA) obj;
            return this.BoolProperty.Equals(inst.BoolProperty) && this.IntProperty.Equals(inst.IntProperty)
                                                               && this.FloatProperty.Equals(inst.FloatProperty)
                                                               && this.StringProperty.Equals(inst.StringProperty);
        }
    }
}