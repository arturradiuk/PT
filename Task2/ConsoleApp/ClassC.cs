﻿using System;
using System.Runtime.Serialization;

namespace ConsoleApp
{
    public class ClassC : ISerializable
    {
        public string StringProperty { get; set; }
        public float FloatProperty { get; set; }
        public int IntProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }
        public ClassA AProperty { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("StringProperty", StringProperty, typeof(string));
            info.AddValue("FloatProperty", FloatProperty, typeof(float));
            info.AddValue("IntProperty", IntProperty, typeof(int));
            info.AddValue("DateTimeProperty", DateTimeProperty, typeof(DateTime));
            info.AddValue("AProperty", AProperty, typeof(ClassA));
        }
    }
}