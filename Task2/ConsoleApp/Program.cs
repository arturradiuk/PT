using System;
using System.IO;
using System.Runtime.Serialization;
using OwnSerializerLib;
using XMLSerializerLib;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose option \nown - for own serializer \nxml - for xml serializer");
            String option = Console.ReadLine();
            switch (option)
            {
                case "own":
                {
                    Console.WriteLine("Choose option \ns - for serialization \nd - for deserialization");
                    String localOption = Console.ReadLine();
                    switch (localOption)
                    {
                        case "s":
                        {
                            Console.WriteLine(
                                "Choose option \nA - for class A serialization \nB - for class B serialization \nC - for class C serialization");
                            String localClassOption = Console.ReadLine();

                            ClassA classA = new ClassA("message from A class", 56.35345f, 65, false, null);
                            ClassB classB = new ClassB("message from B class", 57.35345f, 66, true, null);
                            ClassC classC = new ClassC("message from C class", 58.35345f, 67, false, null);

                            classA.BProperty = classB;
                            classB.CProperty = classC;
                            classC.AProperty = classA;

                            switch (localClassOption)
                            {
                                case "A":
                                {
                                    using (FileStream fs = new FileStream("output.txt", FileMode.Create))
                                    {
                                        IFormatter ownSerializer = new Serializer();
                                        ownSerializer.Serialize(fs, classA);
                                    }

                                    Console.WriteLine("Success");
                                    break;
                                }
                                case "B":
                                {
                                    using (FileStream fs = new FileStream("output.txt", FileMode.Create))
                                    {
                                        IFormatter ownSerializer = new Serializer();
                                        ownSerializer.Serialize(fs, classB);
                                    }

                                    Console.WriteLine("Success");
                                    break;
                                }
                                case "C":
                                {
                                    using (FileStream fs = new FileStream("output.txt", FileMode.Create))
                                    {
                                        IFormatter ownSerializer = new Serializer();
                                        ownSerializer.Serialize(fs, classC);
                                    }

                                    Console.WriteLine("Success");
                                    break;
                                }
                            }

                            break;
                        }

                        case "d":
                        {
                            Console.WriteLine(
                                "Choose option \nA - for class A deserialization \nB - for class B deserialization \nC - for class C deserialization");
                            String local_class_option = Console.ReadLine();
                            switch (local_class_option)
                            {
                                case "A":
                                {
                                    ClassA test;
                                    using (FileStream fs = new FileStream("output.txt", FileMode.Open))
                                    {
                                        IFormatter ownSerializer = new Serializer();
                                        test = ownSerializer.Deserialize(fs) as ClassA;
                                    }

                                    Console.WriteLine(test.ToString());
                                    Console.WriteLine(test.BProperty.ToString());
                                    Console.WriteLine(test.BProperty.CProperty.ToString());

                                    Console.WriteLine("Success");
                                    break;
                                }
                                case "B":
                                {
                                    ClassB test;
                                    using (FileStream fs = new FileStream("output.txt", FileMode.Open))
                                    {
                                        IFormatter ownSerializer = new Serializer();
                                        test = ownSerializer.Deserialize(fs) as ClassB;
                                    }

                                    Console.WriteLine(test.ToString());
                                    Console.WriteLine(test.CProperty.ToString());
                                    Console.WriteLine(test.CProperty.AProperty.ToString());

                                    Console.WriteLine("Success");
                                    break;
                                }
                                case "C":
                                {
                                    ClassC test;
                                    using (FileStream fs = new FileStream("output.txt", FileMode.Open))
                                    {
                                        IFormatter ownSerializer = new Serializer();
                                        test = ownSerializer.Deserialize(fs) as ClassC;
                                    }

                                    Console.WriteLine(test.ToString());
                                    Console.WriteLine(test.AProperty.ToString());
                                    Console.WriteLine(test.AProperty.BProperty.ToString());

                                    Console.WriteLine("Success");
                                    break;
                                }
                            }

                            break;
                        }
                    }

                    break;
                }


                case "xml":
                {
                    Console.WriteLine("Choose option \ns - for serialization \nd - for deserialization");
                    String local_option = Console.ReadLine();
                    switch (local_option)
                    {
                        case "s":
                        {
                            Console.WriteLine(
                                "Choose option \nA - for class A serialization \nB - for class B serialization \nC - for class C serialization");
                            String local_class_option = Console.ReadLine();

                            ClassA classA = new ClassA("message from A class", 56.35345f, 65, false, null);
                            ClassB classB = new ClassB("message from B class", 57.35345f, 66, true, null);
                            ClassC classC = new ClassC("message from C class", 58.35345f, 67, false, null);

                            classA.BProperty = classB;
                            classB.CProperty = classC;
                            classC.AProperty = classA;

                            switch (local_class_option)
                            {
                                case "A":
                                {
                                    XmlSerializer f = new XmlSerializer();
                                    f.Serialize("output.xml", classA);
                                    Console.WriteLine("Success");
                                    break;
                                }
                                case "B":
                                {
                                    XmlSerializer f = new XmlSerializer();
                                    f.Serialize("output.xml", classB);
                                    Console.WriteLine("Success");
                                    break;
                                }
                                case "C":
                                {
                                    XmlSerializer f = new XmlSerializer();
                                    f.Serialize("output.xml", classC);
                                    Console.WriteLine("Success");
                                    break;
                                }
                            }

                            break;
                        }

                        case "d":
                        {
                            Console.WriteLine(
                                "Choose option \nA - for class A deserialization \nB - for class B deserialization \nC - for class C deserialization");
                            String local_class_option = Console.ReadLine();


                            switch (local_class_option)
                            {
                                case "A":
                                {
                                    XmlSerializer f = new XmlSerializer();
                                    ClassA test = (ClassA) f.Deserialize("output.xml", typeof(ClassA));
                                    Console.WriteLine(test.ToString());
                                    Console.WriteLine(test.BProperty.ToString());
                                    Console.WriteLine(test.BProperty.CProperty.ToString());

                                    Console.WriteLine("Success");
                                    break;
                                }
                                case "B":
                                {
                                    XmlSerializer f = new XmlSerializer();
                                    ClassB test = (ClassB) f.Deserialize("output.xml", typeof(ClassB));
                                    Console.WriteLine(test.ToString());
                                    Console.WriteLine(test.CProperty.ToString());
                                    Console.WriteLine(test.CProperty.AProperty.ToString());
                                    Console.WriteLine("Success");
                                    break;
                                }
                                case "C":
                                {
                                    XmlSerializer f = new XmlSerializer();
                                    ClassC test = (ClassC) f.Deserialize("output.xml", typeof(ClassC));
                                    Console.WriteLine(test.ToString());
                                    Console.WriteLine(test.AProperty.ToString());
                                    Console.WriteLine(test.AProperty.BProperty.ToString());
                                    Console.WriteLine("Success");
                                    break;
                                }
                            }

                            break;
                        }
                    }

                    break;
                }
                default: break;
            }
        }
    }
}