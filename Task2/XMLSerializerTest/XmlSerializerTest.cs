using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using ConsoleApp;

namespace XMLSerializerTest
{
    [TestClass]
    public class XmlSerializerTest
    {
        [TestMethod] public void ClassASerializerTest()
        {
            ClassA classA = new ClassA("message from A class", 56.35345f, 65, false, null);
            ClassB classB = new ClassB("message from B class", 57.35345f, 66, true, null);
            ClassC classC = new ClassC("message from C class", 58.35345f, 67, false, null);

            classA.BProperty = classB;
            classB.CProperty = classC;
            classC.AProperty = classA;
            
            XMLSerializerLib.XmlSerializer f = new XMLSerializerLib.XmlSerializer();
            f.Serialize("test_output.xml", classA);
            
            ClassA test = (ClassA) f.Deserialize("test_output.xml", typeof(ClassA));

            Assert.IsTrue(classA.Equals(test));
            Assert.IsTrue(classB.Equals(test.BProperty));
            Assert.IsTrue(classC.Equals(test.BProperty.CProperty));
            
            File.Delete("test_output.xml");
        }
        
        [TestMethod] public void ClassBSerializerTest()
        {
            ClassA classA = new ClassA("message from A class", 56.35345f, 65, false, null);
            ClassB classB = new ClassB("message from B class", 57.35345f, 66, true, null);
            ClassC classC = new ClassC("message from C class", 58.35345f, 67, false, null);

            classA.BProperty = classB;
            classB.CProperty = classC;
            classC.AProperty = classA;
            
            XMLSerializerLib.XmlSerializer f = new XMLSerializerLib.XmlSerializer();
            f.Serialize("test_output.xml", classB);
            
            ClassB test = (ClassB) f.Deserialize("test_output.xml", typeof(ClassB));

            Assert.IsTrue(classB.Equals(test));
            Assert.IsTrue(classC.Equals(test.CProperty));
            Assert.IsTrue(classA.Equals(test.CProperty.AProperty));
            
            File.Delete("test_output.xml");
        }       
        [TestMethod] public void ClassCSerializerTest()
        {
            ClassA classA = new ClassA("message from A class", 56.35345f, 65, false, null);
            ClassB classB = new ClassB("message from B class", 57.35345f, 66, true, null);
            ClassC classC = new ClassC("message from C class", 58.35345f, 67, false, null);

            classA.BProperty = classB;
            classB.CProperty = classC;
            classC.AProperty = classA;
            
            XMLSerializerLib.XmlSerializer f = new XMLSerializerLib.XmlSerializer();
            f.Serialize("test_output.xml", classC);
            
            ClassC test = (ClassC) f.Deserialize("test_output.xml", typeof(ClassC));

            Assert.IsTrue(classC.Equals(test));
            Assert.IsTrue(classA.Equals(test.AProperty));
            Assert.IsTrue(classB.Equals(test.AProperty.BProperty));
            
            File.Delete("test_output.xml");
        }
    }
}
