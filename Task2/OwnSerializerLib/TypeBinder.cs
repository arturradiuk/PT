using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace OwnSerializerLib
{
    public class TypeBinder : SerializationBinder
    {
        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = serializedType.Assembly.FullName;
            typeName = serializedType.FullName;
        }
        
        public override Type BindToType(string assemblyName, string typeName)
        {
            return Assembly.Load(assemblyName).GetType(typeName);
        }
    }
}