using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using ConsoleApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OwnSerializerLib;

namespace OwnSerializerTest
{
    [TestClass]
    public class OwnSerializerTest
    {
        [TestMethod]
        public void ClassASerializeTest()
        {
            ClassA classA = new ClassA("message from A class", 56.35345f, 65, false, null);
            ClassB classB = new ClassB("message from B class", 57.35345f, 66, true, null);
            ClassC classC = new ClassC("message from C class", 58.35345f, 67, false, null);

            classA.BProperty = classB;
            classB.CProperty = classC;
            classC.AProperty = classA;
            using (FileStream fs = new FileStream("test_output.txt", FileMode.Create))
            {
                IFormatter ownSerializer = new Serializer();
                ownSerializer.Serialize(fs, classA);
            }

            List<String> temp = new List<String>();
            using (StreamReader reader = new StreamReader(new FileStream("test_output.txt", FileMode.Open)))
            {
                String l;
                while ((l = reader.ReadLine()) != null)
                {
                    temp.Add(l);
                }
            }

            Assert.AreEqual(3, temp.Count);
            Assert.AreEqual(temp[0],
                "{ConsoleApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}	{ConsoleApp.ClassA}	{m_idGenerator:\"1\"}{System.String:StringProperty:\"message from A class\"}{System.Single:FloatProperty:\"56.35345\"}{System.Int32:IntProperty:\"65\"}{System.Boolean:BoolProperty:\"False\"}{ConsoleApp.ClassB:BProperty:\"2\"}");
            Assert.AreEqual(temp[1],
                "{ConsoleApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}	{ConsoleApp.ClassB}	{m_idGenerator:\"2\"}{System.String:StringProperty:\"message from B class\"}{System.Single:FloatProperty:\"57.35345\"}{System.Int32:IntProperty:\"66\"}{System.Boolean:BoolProperty:\"True\"}{ConsoleApp.ClassC:CProperty:\"3\"}");
            Assert.AreEqual(temp[2],
                "{ConsoleApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}	{ConsoleApp.ClassC}	{m_idGenerator:\"3\"}{System.String:StringProperty:\"message from C class\"}{System.Single:FloatProperty:\"58.35345\"}{System.Int32:IntProperty:\"67\"}{System.Boolean:BoolProperty:\"False\"}{ConsoleApp.ClassA:AProperty:\"1\"}");
            File.Delete("test_output.txt");
        }

        [TestMethod]
        public void ClassBSerializeTest()
        {
            ClassA classA = new ClassA("message from A class", 56.35345f, 65, false, null);
            ClassB classB = new ClassB("message from B class", 57.35345f, 66, true, null);
            ClassC classC = new ClassC("message from C class", 58.35345f, 67, false, null);

            classA.BProperty = classB;
            classB.CProperty = classC;
            classC.AProperty = classA;

            using (FileStream fs = new FileStream("test_output.txt", FileMode.Create))
            {
                IFormatter ownSerializer = new Serializer();
                ownSerializer.Serialize(fs, classB);
            }

            List<String> temp = new List<String>();
            using (StreamReader reader = new StreamReader(new FileStream("test_output.txt", FileMode.Open)))
            {
                String l;
                while ((l = reader.ReadLine()) != null)
                {
                    temp.Add(l);
                }
            }

            Assert.AreEqual(3, temp.Count);
            Assert.AreEqual(temp[0],
                "{ConsoleApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}	{ConsoleApp.ClassB}	{m_idGenerator:\"1\"}{System.String:StringProperty:\"message from B class\"}{System.Single:FloatProperty:\"57.35345\"}{System.Int32:IntProperty:\"66\"}{System.Boolean:BoolProperty:\"True\"}{ConsoleApp.ClassC:CProperty:\"2\"}");
            Assert.AreEqual(temp[1],
                "{ConsoleApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}	{ConsoleApp.ClassC}	{m_idGenerator:\"2\"}{System.String:StringProperty:\"message from C class\"}{System.Single:FloatProperty:\"58.35345\"}{System.Int32:IntProperty:\"67\"}{System.Boolean:BoolProperty:\"False\"}{ConsoleApp.ClassA:AProperty:\"3\"}");
            Assert.AreEqual(temp[2],
                "{ConsoleApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}	{ConsoleApp.ClassA}	{m_idGenerator:\"3\"}{System.String:StringProperty:\"message from A class\"}{System.Single:FloatProperty:\"56.35345\"}{System.Int32:IntProperty:\"65\"}{System.Boolean:BoolProperty:\"False\"}{ConsoleApp.ClassB:BProperty:\"1\"}");
            File.Delete("test_output.txt");
        }

        [TestMethod]
        public void ClassCSerializeTest()
        {
            ClassA classA = new ClassA("message from A class", 56.35345f, 65, false, null);
            ClassB classB = new ClassB("message from B class", 57.35345f, 66, true, null);
            ClassC classC = new ClassC("message from C class", 58.35345f, 67, false, null);

            classA.BProperty = classB;
            classB.CProperty = classC;
            classC.AProperty = classA;

            using (FileStream fs = new FileStream("test_output.txt", FileMode.Create))
            {
                IFormatter ownSerializer = new Serializer();
                ownSerializer.Serialize(fs, classC);
            }

            List<String> temp = new List<String>();
            using (StreamReader reader = new StreamReader(new FileStream("test_output.txt", FileMode.Open)))
            {
                String l;
                while ((l = reader.ReadLine()) != null)
                {
                    temp.Add(l);
                }
            }

            Assert.AreEqual(3, temp.Count);
            Assert.AreEqual(temp[0],
                "{ConsoleApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}	{ConsoleApp.ClassC}	{m_idGenerator:\"1\"}{System.String:StringProperty:\"message from C class\"}{System.Single:FloatProperty:\"58.35345\"}{System.Int32:IntProperty:\"67\"}{System.Boolean:BoolProperty:\"False\"}{ConsoleApp.ClassA:AProperty:\"2\"}");
            Assert.AreEqual(temp[1],
                "{ConsoleApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}	{ConsoleApp.ClassA}	{m_idGenerator:\"2\"}{System.String:StringProperty:\"message from A class\"}{System.Single:FloatProperty:\"56.35345\"}{System.Int32:IntProperty:\"65\"}{System.Boolean:BoolProperty:\"False\"}{ConsoleApp.ClassB:BProperty:\"3\"}");
            Assert.AreEqual(temp[2],
                "{ConsoleApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}	{ConsoleApp.ClassB}	{m_idGenerator:\"3\"}{System.String:StringProperty:\"message from B class\"}{System.Single:FloatProperty:\"57.35345\"}{System.Int32:IntProperty:\"66\"}{System.Boolean:BoolProperty:\"True\"}{ConsoleApp.ClassC:CProperty:\"1\"}");
            File.Delete("test_output.txt");
        }

        [TestMethod]
        public void ClassADeserializeTest()
        {
            ClassA classA = new ClassA("message from A class", 56.35345f, 65, false, null);
            ClassB classB = new ClassB("message from B class", 57.35345f, 66, true, null);
            ClassC classC = new ClassC("message from C class", 58.35345f, 67, false, null);

            classA.BProperty = classB;
            classB.CProperty = classC;
            classC.AProperty = classA;

            using (FileStream fs = new FileStream("test_output.txt", FileMode.Create))
            {
                IFormatter ownSerializer = new Serializer();
                ownSerializer.Serialize(fs, classA);
            }

            ClassA test;
            using (FileStream fs = new FileStream("test_output.txt", FileMode.Open))
            {
                IFormatter ownSerializer = new Serializer();
                test = ownSerializer.Deserialize(fs) as ClassA;
            }

            Assert.IsTrue(classA.Equals(test));
            Assert.IsTrue(classB.Equals(test.BProperty));
            Assert.IsTrue(classC.Equals(test.BProperty.CProperty));

            File.Delete("test_output.txt");
        }

        [TestMethod]
        public void ClassBDeserializeTest()
        {
            ClassA classA = new ClassA("message from A class", 56.35345f, 65, false, null);
            ClassB classB = new ClassB("message from B class", 57.35345f, 66, true, null);
            ClassC classC = new ClassC("message from C class", 58.35345f, 67, false, null);

            classA.BProperty = classB;
            classB.CProperty = classC;
            classC.AProperty = classA;

            using (FileStream fs = new FileStream("test_output.txt", FileMode.Create))
            {
                IFormatter ownSerializer = new Serializer();
                ownSerializer.Serialize(fs, classB);
            }

            ClassB test;
            using (FileStream fs = new FileStream("test_output.txt", FileMode.Open))
            {
                IFormatter ownSerializer = new Serializer();
                test = ownSerializer.Deserialize(fs) as ClassB;
            }

            Assert.IsTrue(classB.Equals(test));
            Assert.IsTrue(classC.Equals(test.CProperty));
            Assert.IsTrue(classA.Equals(test.CProperty.AProperty));
            File.Delete("test_output.txt");
        }

        [TestMethod]
        public void ClassCDeserializeTest()
        {
            ClassA classA = new ClassA("message from A class", 56.35345f, 65, false, null);
            ClassB classB = new ClassB("message from B class", 57.35345f, 66, true, null);
            ClassC classC = new ClassC("message from C class", 58.35345f, 67, false, null);

            classA.BProperty = classB;
            classB.CProperty = classC;
            classC.AProperty = classA;

            using (FileStream fs = new FileStream("test_output.txt", FileMode.Create))
            {
                IFormatter ownSerializer = new Serializer();
                ownSerializer.Serialize(fs, classC);
            }

            ClassC test;
            using (FileStream fs = new FileStream("test_output.txt", FileMode.Open))
            {
                IFormatter ownSerializer = new Serializer();
                test = ownSerializer.Deserialize(fs) as ClassC;
            }

            Assert.IsTrue(classC.Equals(test));
            Assert.IsTrue(classA.Equals(test.AProperty));
            Assert.IsTrue(classB.Equals(test.AProperty.BProperty));
            File.Delete("test_output.txt");
        }
    }
}