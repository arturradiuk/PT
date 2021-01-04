using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Task3;

namespace Task3Tests
{
    [TestClass]
    public class DataServiceTests
    {
        [TestMethod]
        public void GetProductsByNameTest()
        {
            string pattern = "Cha";
            List<Product> products = DataService.GetProductsByName(pattern);
            Assert.AreEqual(5, products.Count);
            foreach (Product product in products)
            {
                Assert.IsTrue(product.Name.Contains(pattern));
            }
        }

        [TestMethod]
        public void GetProductsByVendorNameTest()
        {
            List<Product> products = DataService.GetProductsByVendorName("Australia Bike Retailer");
            Assert.AreEqual(16, products.Count);
            //SQL query:
            //select count(*) as number from Purchasing.ProductVendor
            //inner join Purchasing.Vendor on ProductVendor.BusinessEntityID = Vendor.BusinessEntityID
            //where Name == 'Australia Bike Retailer'
        }

        [TestMethod]
        public void GetProductNamesByVendorNameTest()
        {
            List<string> names = DataService.GetProductNamesByVendorName("Australia Bike Retailer");
            Assert.AreEqual(16, names.Count);
            foreach (string name in names)
            {
                Assert.IsTrue(name.Contains("Thin-Jam Lock Nut"));
            }
            
            //SQL query:
            // select Product.Name
            // from Purchasing.ProductVendor
            //     inner join Purchasing.Vendor on ProductVendor.BusinessEntityID = Vendor.BusinessEntityID
            // inner join Production.Product on Purchasing.ProductVendor.ProductID = Product.ProductID
            // where Vendor.Name = 'Australia Bike Retailer' 
        }

        [TestMethod]
        public void GetProductVendorByProductNameTest()
        {
            string product = DataService.GetProductVendorByProductName("Water Bottle - 30 oz.");
            Assert.AreEqual("Green Lake Bike Company", product);
            
            //SQL query:
            // select Vendor.Name
            //     from Purchasing.Vendor
            //     inner join Purchasing.ProductVendor on Vendor.BusinessEntityID = ProductVendor.BusinessEntityID
            // inner join Production.Product on ProductVendor.ProductID = Product.ProductID
            // where Product.Name = 'Water Bottle - 30 oz.'
        }

        [TestMethod]
        public void GetProductsWithNRecentReviewsTest()
        {
            List<Product> products = DataService.GetProductsWithNRecentReviews(1);
            Assert.AreEqual(2, products.Count);
            
            //SQL query:
            // select Production.Product.*
            //     from Production.Product
            //     where Product.ProductID in
            // (select Product.ProductID
            //     from Production.ProductReview
            //     inner join Production.Product on Production.ProductReview.ProductID = Production.Product.ProductID
            // group by Production.Product.ProductID
            //     having count(ProductReview.ProductID) = 1)
        }

        [TestMethod]
        public void GetNRecentlyReviewedProductsTest()
        {
            List<Product> products = DataService.GetNRecentlyReviewedProducts(3);
            Assert.AreEqual(709, products[0].ProductID);
            Assert.AreEqual(798, products[1].ProductID);
            Assert.AreEqual(937, products[2].ProductID);
        }

        [TestMethod]
        public void GetNProductsFromCategoryTest()
        {
            List<Product> products = DataService.GetNProductsFromCategory("Components", 4);
            Assert.AreEqual(products.Distinct().Count(), products.Count);
            foreach (Product product in products)
            {
                Assert.AreEqual("Components", product.ProductSubcategory.ProductCategory.Name);
            }

            //SQL query:
            // select top 4 Product.*
            //     from Production.Product
            //         inner join Production.ProductSubcategory
            // on Product.ProductSubcategoryID = ProductSubcategory.ProductSubcategoryID
            // inner join Production.ProductCategory on ProductSubcategory.ProductCategoryID = ProductCategory.ProductCategoryID
            // where ProductCategory.Name = 'Components'
        }

        [TestMethod]
        public void GetTotalStandardCostByCategoryTest()
        {
            double totalCost = DataService.GetTotalStandardCostByCategory("Components");
            Assert.AreEqual(35930.3944, totalCost);
            
            //SQL query:
            // select sum(StandardCost)
            // from Production.Product
            //     inner join Production.ProductSubcategory
            // on Product.ProductSubcategoryID = ProductSubcategory.ProductSubcategoryID
            // inner join Production.ProductCategory
            //     on ProductSubcategory.ProductCategoryID = ProductCategory.ProductCategoryID
            // where ProductCategory.Name = 'Components'
        }
    }
}