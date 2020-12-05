using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace OwnSerializerLib
{
    public class TypeBinder : SerializationBinder
    {
        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            Assembly asm = serializedType.Assembly;
            assemblyName = asm.FullName;
            typeName = serializedType.FullName;
        }


        public override Type BindToType(string assemblyName, string typeName)
        {
            Assembly asm = Assembly.Load(assemblyName);
            return asm.GetType(typeName);
        }
    }
}