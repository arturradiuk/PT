using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using OwnSerializerLib;
using XMLSerializerLib;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ClassA classA = new ClassA();
            classA.StringProperty = "message from A class";
            classA.FloatProperty = 56.35345f;
            classA.IntProperty = 65;
            classA.BoolProperty = false;


            ClassB classB = new ClassB();
            classB.StringProperty = "message from B class";
            classB.FloatProperty = 57.35345f;
            classB.IntProperty = 66;
            classB.BoolProperty = true;


            ClassC classC = new ClassC();
            classC.StringProperty = "message from C class";
            classC.FloatProperty = 58.35345f;
            classC.IntProperty = 67;
            classC.BoolProperty = false;

            classA.BProperty = classB;
            classB.CProperty = classC;
            classC.AProperty = classA;
            
            
            
            XMLSerializerLib.XmlSerializer f = new XMLSerializerLib.XmlSerializer();
            f.Serialize("output.xml", classA);
            ClassA temp = (ClassA) f.Deserialize("output.xml", typeof(ClassA));
            
            Console.WriteLine(temp);
            Console.WriteLine(temp.BProperty);
            Console.WriteLine(temp.BProperty.CProperty);
            Console.WriteLine(temp.BProperty.CProperty.AProperty);

            // using (FileStream s= new FileStream("output.txt", FileMode.Create))
            // {
            // XMLSerializerLib.Serializer f = new XMLSerializerLib.Serializer()
            // IFormatter f = new OwnSerializerLib.Serializer();
            // f.Serialize(s,classA);
            // }

            // ClassA A;
            // using (FileStream s = new FileStream("output.txt", FileMode.Open))
            // {
            // XMLSerializerLib.Serializer f = new XMLSerializerLib.Serializer()
            //     IFormatter f = new OwnSerializerLib.Serializer();
            //     A = (ClassA) f.Deserialize(s);
            // }
            //
            // Console.WriteLine(A.ToString());
            // Console.WriteLine(A.BProperty);
            // Console.WriteLine(A.BProperty.CProperty);


            // ClassA classA = new ClassA();

            // using (FileStream s = new FileStream("output.txt", FileMode.Open))
            // {
            // IFormatter f = new Serializer();
            // ClassA testClass = (ClassA)f.Deserialize(s);
            // Console.WriteLine(testClass.StringProperty);
            // Console.WriteLine(testClass.FloatProperty);
            // Console.WriteLine(testClass.IntProperty);
            // Console.WriteLine(testClass.BoolProperty);
            // }
        }
    }
}