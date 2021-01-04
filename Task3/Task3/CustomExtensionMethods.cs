using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Task3
{
    public static class CustomExtensionMethods
    {
        public static List<Product> GetProductsWithoutCategory_QS(this List<Product> products)
        {
            return (from product in products where product.ProductSubcategoryID == null select product).ToList();
        }

        public static List<Product> GetProductsWithoutCategory_MS(this List<Product> products)
        {
            return products.Where(product => product.ProductSubcategoryID == null).ToList();
        }

        public static List<Product> GetProductsPage(this List<Product> products, int pageSize, int pageNum)
        {
            return products.Skip(pageSize * (pageNum - 1)).Take(pageSize).ToList();
        }

        public static string GetProductNamesAndVendors(this List<Product> products)
        {
            string resultString = "";
            LocalDataContext data = new LocalDataContext();
            var results = from product in products
                select new {productName = product.Name, productVendors = product.ProductVendors};

            foreach (var item in results)
            {
                List<string> vendorNames = new List<string>();
                foreach (ProductVendor productVendor in item.productVendors)
                {
                    vendorNames.Add(productVendor.Vendor.Name);
                }

                string vendorsString = vendorNames.Count > 0
                    ? String.Join(", ", vendorNames)
                    : "No vendors for this product";
                
                
                resultString += $"{item.productName} - {vendorsString} \n";
            }

            return resultString.Trim();
        }
    }
}