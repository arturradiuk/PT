using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using OwnSerializerLib;
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




            using (FileStream s = new FileStream("output.txt", FileMode.Create))
            {
                IFormatter f = new Serializer();
                f.Serialize(s, classC);
            }
        }
    }
}
