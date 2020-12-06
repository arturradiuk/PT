﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
            this.m_idGenerator.GetId(graph, out bool firstTime);

            foreach (PropertyInfo propertyInfo in properties)
            {
                WriteMember(propertyInfo.Name,propertyInfo.GetValue(graph));
            }


            this._dataSB.Append("\n");

            while (this.m_objectQueue.Count != 0)
            {
                this.Serialize(this.m_objectQueue.Dequeue());
            }

        
        }

        public override object Deserialize(Stream serializationStream)
        {
            throw new NotImplementedException();
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
                                    "\""+this.m_idGenerator.GetId(obj, out bool firstTime).ToString()+"\"" + "}");

                if (firstTime)
                {
                    this.m_objectQueue.Enqueue(obj);
                }
            }
            else
            {
                this._dataSB.Append("{" + "null" + ":" + name + ":"+"\""+"null"+"\"");
            }
        }

        protected void WriteString(object obj, string name)
        {
            this._dataSB.Append("{" + obj.GetType() + ":" + name + ":" + "\"" + (String) obj + "\"" + "}");
        }

        protected override void WriteSingle(float val, string name)
        {
            this._dataSB.Append("{" + val.GetType() + ":" + name + ":" +"\""+ val.ToString()+"\"" + "}");
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