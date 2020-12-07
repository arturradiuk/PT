using System.Runtime.Serialization;

namespace ConsoleApp
{
    public class ClassA : ISerializable
    {
        public string StringProperty { get; set; }
        public float FloatProperty { get; set; }
        public int IntProperty { get; set; }
        public bool BoolProperty { get; set; }
        public ClassB BProperty { get; set; }

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
    }
}