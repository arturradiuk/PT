using System.Collections.Generic;
using System.Linq;

namespace Task3
{
    public class DataService
    {
        private static LocalDataContext data = new LocalDataContext();


        public static List<Product> GetProductsByName(string namePart)
        {
            List<Product> res = (from product in data.GetTable<Product>()
                where product.Name.Contains(namePart)
                select product).ToList();
            return res;
        }

        public static List<Product> GetProductsByVendorName(string vendorName)
        {
            List<Product> res = (from pv in data.GetTable<ProductVendor>()
                where pv.Vendor.Name.Equals(vendorName)
                select pv.Product).ToList();
            return res;
        }

        public static List<string> GetProductNamesByVendorName(string vendorName)
        {
            List<string> res = (from pv in data.GetTable<ProductVendor>()
                where pv.Vendor.Name.Equals(vendorName)
                select pv.Product.Name).ToList();
            return res;
        }

        public static string GetProductVendorByProductName(string productName)
        {
            string res = (from pv in data.GetTable<ProductVendor>()
                where pv.Product.Name.Equals(productName)
                select pv.Vendor.Name).Single();
            return res;
        }

        public static List<Product> GetProductsWithNRecentReviews(int howManyReviews)
        {
            List<Product> res = (from product in data.GetTable<Product>()
                where product.ProductReviews.Count == howManyReviews
                select product).ToList();

            return res;
        }

        public static List<Product> GetNRecentlyReviewedProducts(int howManyProducts)
        {
            List<Product> res = (from product in data.GetTable<ProductReview>()
                orderby product.ReviewDate descending
                select product.Product).Distinct().Take(howManyProducts).ToList();
            return res;
        }

        public static List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            List<Product> res = (from product in data.GetTable<Product>()
                where product.ProductSubcategory.ProductCategory.Name.Equals(categoryName)
                select product).Take(n).ToList();
            return res;
        }

        public static double GetTotalStandardCostByCategory(string category)
        {
            double res = (double) (from product in data.GetTable<Product>()
                where product.ProductSubcategory.ProductCategory.Name.Equals(category)
                select product.StandardCost).ToList().Sum();
            return res;
        }
    }
}