using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace OwnSerializerLib
{
    public class Serializer : Formatter
    {
        public override ISurrogateSelector SurrogateSelector { get; set; }
        public override SerializationBinder Binder { get; set; }

        public override StreamingContext Context
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        private List<string> DeserializeInfoStr = new List<string>();
        private Dictionary<string, object> _typeProperties = new Dictionary<string, object>();


        public Serializer()
        {
            this.Binder = new TypeBinder();
        }

        private StringBuilder _dataSB = new StringBuilder();


        private void WriteStream(Stream serializationStream)
        {
            if (serializationStream != null)
            {
                using (StreamWriter writer = new StreamWriter(serializationStream))
                {
                    writer.Write(this._dataSB);
                }
            }
        }

        private void ReadStream(Stream serializationStream)
        {
            if (serializationStream != null)
            {
                using (StreamReader reader = new StreamReader(serializationStream))
                {
                    String l;
                    while ((l = reader.ReadLine()) != null)
                    {
                        DeserializeInfoStr.Add(l);
                    }
                }
            }
            else throw new ArgumentException("Provided stream is empty or doesn't exist");
        }

        private void FillTypeProperties()
        {
            foreach (string l in DeserializeInfoStr)
            {
                string[] splits = l.Split('{', '}').Where(s => !string.IsNullOrEmpty(s)).ToArray();
                string t = splits[2].Split(':')[1];
            }
        }

        public override void Serialize(Stream serializationStream, object graph) // todo should add version
            // to the serialized objects to ensure the version compatibility?
        {
            this.Serialize(graph);
            this.WriteStream(serializationStream);
        }

        private void Serialize(object graph)
        {
            List<PropertyInfo> properties = graph.GetType().GetProperties().ToList();

            Binder.BindToName(graph.GetType(), out string assemblyName, out string typeName);
            this._dataSB.Append("{" + assemblyName + "}\t{" + typeName + "}\t{m_idGenerator:\"" +
                                this.m_idGenerator.GetId(graph, out bool firstTime) + "\"}\n");

            foreach (PropertyInfo propertyInfo in properties)
            {
                WriteMember(propertyInfo.Name, propertyInfo.GetValue(graph));
            }

            while (this.m_objectQueue.Count != 0)
            {
                this.Serialize(this.m_objectQueue.Dequeue());
            }
        }

        public override object Deserialize(Stream serializationStream)
        {
            ReadStream(serializationStream);
            
            //creating uninitialized objects and adding theirs IDs to dictionary
            object[] objects = new object[DeserializeInfoStr.Count];
            Dictionary<int, object> objectIDs = new Dictionary<int, object>();
            for (int i = 0; i < DeserializeInfoStr.Count; i++)
            {
                string[] splits = DeserializeInfoStr[i].Replace("\t", "").Split('{', '}').Where(s => !string.IsNullOrEmpty(s)).ToArray();
                Type type = Binder.BindToType(splits[0], splits[1]);
                int selfID = (int) TypeConverter(splits[2].Split(':')[1].Replace("\"", ""), typeof(int));

                object uninitializedObject = FormatterServices.GetUninitializedObject(type);
                objects[i] = uninitializedObject;
                objectIDs.Add(selfID, uninitializedObject);
            }

            for (int i = 0; i < DeserializeInfoStr.Count; i++)
            {
                string[] splits = DeserializeInfoStr[i].Replace("\t", "").Split('{', '}')
                    .Where(s => !string.IsNullOrEmpty(s)).ToArray();
                Type type = Binder.BindToType(splits[0], splits[1]);

                List<PropertyInfo> properties = type.GetProperties().ToList();
                Type[] types = new Type[properties.Count];
                object[] values = new object[properties.Count];
                int referenceID = (int) TypeConverter(splits[7].Split(':')[2].Split('"')[1], typeof(int));

                int propertiesStart = 3;
                for (int j = 0; j < splits.Length - propertiesStart; j++)
                {
                    string[] localSplits = splits[j + propertiesStart].Split(':');
                    Type parameterType = properties[j].PropertyType;
                    types[j] = parameterType;
                    string uncastedValue = localSplits[2].Replace("\"", "");
                    
                    values[j] = j != splits.Length - propertiesStart - 1 ?
                         TypeConverter(uncastedValue, parameterType) : objectIDs[referenceID];
                }

                type.GetConstructor(types)?.Invoke(objects[i], values); //conditional access to avoid NPR
            }
            
            return objects[0];
        }

        private object TypeConverter(String val, Type type)
        {
            if (type.Equals(typeof(string)))
            {
                return val;
            }
            else if (type.Equals(typeof(float)))
            {
                return Single.Parse(val);
            }
            else if (type.Equals(typeof(int)))
            {
                return int.Parse(val);
            }
            else if (type.Equals(typeof(bool)))
            {
                return bool.Parse(val);
            }

            return null;
        }


        protected override void WriteBoolean(bool val, string name)
        {
            this._dataSB.Append("{" + val.GetType() + ":" + name + ":" + "\"" + val + "\"" + "}");
        }


        protected override void WriteInt32(int val, string name)
        {
            this._dataSB.Append("{" + val.GetType() + ":" + name + ":" + "\"" + val + "\"" + "}");
        }

        protected override void WriteObjectRef(object obj, string name, Type memberType)
        {
            if (memberType.Equals(typeof(String)))
            {
                WriteString(obj, name);
            }
            else
            {
                WriteObject(obj, name, memberType);
            }
        }

        protected void WriteObject(object obj, string name, Type memberType)
        {
            if (obj != null)
            {
                this._dataSB.Append("{" + memberType + ":" + name + ":" +
                                    "\"" + this.m_idGenerator.GetId(obj, out bool firstTime).ToString() + "\"" + "}");

                if (firstTime)
                {
                    this.m_objectQueue.Enqueue(obj);
                }
            }
            else
            {
                Type tempType;
                switch (name)
                {
                    case "BProperty":
                    {
                        tempType = Binder.BindToType(
                            "ConsoleApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "ConsoleApp.ClassB");
                        break;
                    }
                    case "CProperty":
                    {
                        tempType = Binder.BindToType(
                            "ConsoleApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "ConsoleApp.ClassC");
                        break;
                    }
                    case "AProperty":
                    {
                        tempType = Binder.BindToType(
                            "ConsoleApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "ConsoleApp.ClassA");
                        break;
                    }
                    default:
                    {
                        tempType = typeof(object);
                        break;
                    }
                }

                this._dataSB.Append("{" + tempType + ":" + name + ":" + "\"" + "null" + "\"}");
            }
        }

        protected void WriteString(object obj, string name)
        {
            this._dataSB.Append("{" + obj.GetType() + ":" + name + ":" + "\"" + (String) obj + "\"" + "}");
        }

        protected override void WriteSingle(float val, string name)
        {
            this._dataSB.Append("{" + val.GetType() + ":" + name + ":" + "\"" + val.ToString() + "\"" + "}");
        }


        protected override object GetNext(out long objID)
        {
            return base.GetNext(out objID);
        }

        protected override long Schedule(object obj)
        {
            return base.Schedule(obj);
        }

        #region NotImplementedRegion

        protected override void WriteSByte(sbyte val, string name)
        {
            throw new NotImplementedException();
        }


        protected override void WriteTimeSpan(TimeSpan val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt16(ushort val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt32(uint val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteUInt64(ulong val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteValueType(object obj, string name, Type memberType)
        {
            throw new NotImplementedException();
        }

        protected override void WriteByte(byte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteChar(char val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDateTime(DateTime val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDecimal(decimal val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDouble(double val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInt16(short val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInt64(long val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteArray(object obj, string name, Type memberType)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}