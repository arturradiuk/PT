using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Task3;

namespace Task3Tests
{
    [TestClass]
    public class DataServiceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            DataService data = new DataService();
            Assert.AreEqual(504, data.firstMethod().Count);
        }
    }
}
