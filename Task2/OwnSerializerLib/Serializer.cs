using System;
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
        public override StreamingContext Context { get; set; }

        public Serializer()
        {
            this.Binder = new TypeBinder();
            Context = new StreamingContext(StreamingContextStates.File);
        }
        
        private StringBuilder _dataSB = new StringBuilder();
        private String _dataStr = "";

        private void SaveToDataSB()
        {
            this._dataSB.Append(_dataStr + "\n");
            this._dataStr = "";
        }

        private void SaveToStream(Stream serializationStream)
        {
            using (StreamWriter writer = new StreamWriter(serializationStream))
            {
                    writer.Write(this._dataSB);
            }
        }
        public override void Serialize(Stream serializationStream, object graph)
        {
            ISerializable sGraph = graph as ISerializable;
            SerializationInfo info = new SerializationInfo(graph.GetType(), new FormatterConverter());
            Binder.BindToName(graph.GetType(), out string assemblyName, out string typeName);
            sGraph.GetObjectData(info, Context);
            foreach (SerializationEntry se in info)
            {
                WriteMember(se.Name, se.Value);
            }
            this.SaveToDataSB();
            while (this.m_objectQueue.Count != 0)
            {
                this.Serialize(null,this.m_objectQueue.Dequeue());
            }
        }

        public override object Deserialize(Stream serializationStream)
        {
            throw new NotImplementedException();
        }

        protected override object GetNext(out long objID)
        {
            return base.GetNext(out objID);
        }

        protected override long Schedule(object obj)
        {
            return base.Schedule(obj);
        }

        protected override void WriteArray(object obj, string name, Type memberType)
        {
            throw new NotImplementedException();
        }

        protected override void WriteBoolean(bool val, string name)
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

        protected override void WriteInt32(int val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteInt64(long val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteObjectRef(object obj, string name, Type memberType)
        {
            throw new NotImplementedException();
        }

        protected override void WriteMember(string memberName, object data)
        {
            base.WriteMember(memberName, data);
        }

        protected override void WriteSByte(sbyte val, string name)
        {
            throw new NotImplementedException();
        }

        protected override void WriteSingle(float val, string name)
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
    }
}