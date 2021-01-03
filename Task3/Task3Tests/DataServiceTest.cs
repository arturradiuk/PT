using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Task3;

namespace Task3Tests
{
    [TestClass]
    public class DataServiceTest
    {


        [TestMethod]
        public void GetProductsByNameTest()
        {
            Assert.AreEqual(5,DataService.GetProductsByName("Cha").Count );
        }
        [TestMethod]
        public void GetProductsByVendorNameTest()
        {
            Assert.AreEqual(16,DataService.GetProductsByVendorName("Australia Bike Retailer").Count );
        }



    }
}
