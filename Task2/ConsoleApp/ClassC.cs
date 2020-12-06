﻿using System;
using System.Runtime.Serialization;

namespace ConsoleApp
{
    public class ClassC : ISerializable
    {
        public string StringProperty { get; set; }
        public float FloatProperty { get; set; }
        public int IntProperty { get; set; }
        public bool BoolProperty { get; set; }
        public ClassA AProperty { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("StringProperty", StringProperty, typeof(string));
            info.AddValue("FloatProperty", FloatProperty, typeof(float));
            info.AddValue("IntProperty", IntProperty, typeof(int));
            info.AddValue("BoolProperty", BoolProperty, typeof(bool));
            info.AddValue("AProperty", AProperty, typeof(ClassA));
        }

        public ClassC(string stringProperty, float floatProperty, int intProperty, bool boolProperty, ClassA aProperty)
        {
            StringProperty = stringProperty;
            FloatProperty = floatProperty;
            IntProperty = intProperty;
            BoolProperty = boolProperty;
            AProperty = aProperty;
        }
    }
}