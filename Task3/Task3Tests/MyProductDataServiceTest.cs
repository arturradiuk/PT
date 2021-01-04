using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task3;
using Task3.MyProduct;

namespace Task3Tests
{
    [TestClass]
    public class MyProductDataServiceTest
    {
        private static readonly List<Product> Products = DataService.GetFirstNProducts(500);
        
        [TestMethod]
        public void GetProductsByNameTest()
        {

            MyProductDataService dataService = new MyProductDataService(Products.AsEnumerable()
                .Select(product => new MyProduct(product)).ToList());
           
            string pattern = "Cha";

            List<MyProduct> products = dataService.GetProductsByName(pattern);
            
            Assert.AreEqual(5, products.Count);
            
            foreach (MyProduct product in products)
            {
                Assert.IsTrue(product.Name.Contains(pattern));
            }
            
        }
        
        [TestMethod]
        public void GetNProductsFromCategoryTest()
        {
            MyProductDataService dataService = new MyProductDataService(Products.AsEnumerable()
                .Select(product => new MyProduct(product)).ToList());

            List<MyProduct> products = dataService.GetNProductsFromCategory("Components", 4);
            Assert.AreEqual(products.Distinct().Count(), products.Count);
            foreach (MyProduct product in products)
            {
                Assert.AreEqual("Components", product.ProductSubcategory.ProductCategory.Name);
            }

            
        }
       
        [TestMethod]
        public void GetProductsWithNRecentReviewsTest()
        {
            MyProductDataService dataService = new MyProductDataService(Products.AsEnumerable()
                .Select(product => new MyProduct(product)).ToList());

            List<MyProduct> products = dataService.GetProductsWithNRecentReviews(1);
            Assert.AreEqual(2, products.Count);
            
        }
    }
}