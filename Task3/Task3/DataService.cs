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
            List<Product> res = (from pr in data.GetTable<ProductReview>()
                orderby pr.ReviewDate descending
                select pr.Product).Take(howManyReviews).Distinct().ToList();
            return res;
        }

        public static List<Product> GetNRecentlyReviewedProducts(int howManyProducts)
        {
            List<Product> res = (from pr in data.GetTable<ProductReview>()
                orderby pr.ReviewDate descending
                group pr.Product by pr.ProductID
                into g
                select g.First()).Take(howManyProducts).ToList();
            return res;
        }

        public static List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            List<Product> res = (from product in data.GetTable<Product>()
                where product.ProductSubcategory.ProductCategory.Name.Equals(categoryName)
                select product).Take(n).ToList();
            return res;
        }

        public static int GetTotalStandardCostByCategory(ProductCategory category)
        {
            int res = (int) (from product in data.GetTable<Product>()
                where product.ProductSubcategory.ProductCategory.Name.Equals(category.Name)
                select product.StandardCost).ToList().Sum();
            return res;
        }
    }
}