using System;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;

namespace XMLSerializerLib
{
    public class XmlSerializer
    {

        public object Deserialize(String fileName, Type type)
        {
            object res;
            FileStream fs = new FileStream(fileName, FileMode.Open);
            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            DataContractSerializer ser = new DataContractSerializer(type);
            res =  ser.ReadObject(reader, true);
            reader.Close();
            fs.Close();
            return res;
        }

        public void Serialize(String fileName, object graph)
        {
            DataContractSerializer ser = new DataContractSerializer(graph.GetType(), null, Int32.MaxValue, false, true, null, null);
            XmlWriter writer = XmlWriter.Create(fileName, new XmlWriterSettings() { Indent = true });
            ser.WriteObject(writer, graph);
            writer.Close();
        }
        
        

    }
}
