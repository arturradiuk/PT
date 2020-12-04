using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//todo MS says that BinaryFormatter is unsafe and should not be used by any means,
// https://docs.microsoft.com/pl-pl/dotnet/standard/serialization/binaryformatter-security-guide
// they generally not recommend binary formatters.
// Should we care or just disregard it as that is a part of the task

namespace Serializer
{
    public static class BinarySerializer
    {
        public static void SerializeToBinary(object obj, string filePath)
        {
            FileStream fileStream;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            // todo handle better overwriting
            if (File.Exists(filePath)) File.Delete(filePath);
            fileStream = File.Create(filePath);
            binaryFormatter.Serialize(fileStream, obj);
            fileStream.Close();
        }

        public static object DeserializeFromBinary(string filePath)
        {
            if (!File.Exists(filePath)) throw new FileLoadException($"file {filePath} could not be loaded properly");
            
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.OpenRead(filePath);
            object obj = binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return obj;
        }
    }
}