using System;
using System.Runtime.Serialization;

namespace ConsoleApp
{
    // [DataContract]

    public class ClassB
    {        
        [DataMember]

        public string StringProperty { get; set; }
        [DataMember]

        public float FloatProperty { get; set; }
        [DataMember]

        public int IntProperty { get; set; }
        [DataMember]

        public bool BoolProperty { get; set; }
        [DataMember]

        public ClassC CProperty { get; set; }

        public ClassB(string stringProperty, float floatProperty, int intProperty, bool boolProperty, ClassC cProperty)
        {
            StringProperty = stringProperty;
            FloatProperty = floatProperty;
            IntProperty = intProperty;
            BoolProperty = boolProperty;
            CProperty = cProperty;
        }
                public ClassB(){}


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("StringProperty", StringProperty, typeof(string));
            info.AddValue("FloatProperty", FloatProperty, typeof(float));
            info.AddValue("IntProperty", IntProperty, typeof(int));
            info.AddValue("BoolProperty", BoolProperty, typeof(bool));
            info.AddValue("CProperty", CProperty, typeof(ClassC));
        }
        public override string ToString()
        {
            return $"{StringProperty}=StringProperty,{FloatProperty}=FloatProperty,{IntProperty}=IntProperty,{BoolProperty}=BoolProperty";
        }
    }
}