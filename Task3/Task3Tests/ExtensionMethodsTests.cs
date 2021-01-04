using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task3;

namespace Task3Tests
{
    [TestClass]
    public class ExtensionMethodsTests
    {
        private static readonly List<Product> Products = DataService.GetFirstNProducts(500);

        [TestMethod]
        public void GetProductsWithoutCategoryTest()
        {
            List<Product> productsQS = Products.GetProductsWithoutCategory_QS();
            List<Product> productsMS = Products.GetProductsWithoutCategory_MS();

            Assert.IsTrue(productsMS.SequenceEqual(productsMS));
            Assert.AreEqual(209, productsMS.Count);

            //SQL query:
            // select Product.*
            // from Production.Product
            // where ProductSubcategoryID is null
        }

        [TestMethod]
        public void GetGetProductsWithoutCategoryTest()
        {
            List<Product> firstProductsPage = Products.GetProductsPage(15, 1);
            List<Product> secondProductsPage = Products.GetProductsPage(15, 2);
            List<Product> doubleProductsPage = Products.GetProductsPage(30, 1);

            Assert.AreEqual(15, firstProductsPage.Count);
            Assert.AreEqual(15, secondProductsPage.Count);
            Assert.AreEqual(30, doubleProductsPage.Count);
            Assert.AreEqual(0, firstProductsPage.Intersect(secondProductsPage).Count());
            Assert.IsTrue(doubleProductsPage.SequenceEqual(firstProductsPage.Concat(secondProductsPage)));
        }

        [TestMethod]
        public void GetProductNamesAndVendorsTest()
        {
            List<Product> productsSublist = Products.GetRange(0, 10);
            string resultString = productsSublist.GetProductNamesAndVendors();
            string expectedString = @"Adjustable Race - Litware, Inc. 
Bearing Ball - Wood Fitness 
BB Ball Bearing - No vendors for this product 
Headset Ball Bearings - American Bicycles and Wheels 
Blade - No vendors for this product 
LL Crankarm - Vision Cycles, Inc., Proseware, Inc. 
ML Crankarm - Vision Cycles, Inc., Proseware, Inc. 
HL Crankarm - West Junction Cycles, Vision Cycles, Inc., Proseware, Inc. 
Chainring Bolts - Training Systems, Beaumont Bikes, Bike Satellite Inc. 
Chainring Nut - Training Systems, Beaumont Bikes, Bike Satellite Inc.".Replace("\r\n", "\n");
            
            Assert.AreEqual(expectedString, resultString);
        }
    }
}