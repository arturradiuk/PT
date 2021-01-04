using System.Collections.Generic;
using System.Linq;

namespace Task3.MyProduct
{
    public class MyProductDataService
    {
        private List<MyProduct> Products;

        public MyProductDataService(List<MyProduct> products)
        {
            Products = products;
        }

        public List<MyProduct> GetProductsByName(string namePart)
        {
            List<MyProduct> res = (from product in Products
                where product.Name.Contains(namePart)
                select product).ToList();
            return res;
        }

        public List<MyProduct> GetNProductsFromCategory(string categoryName, int n)
        {
            List<MyProduct> res = (from product in Products
                where product.ProductSubcategory != null && product.ProductSubcategory.ProductCategory.Name.Equals(categoryName)
                select product).Take(n).ToList();
            return res;
        }

        public List<MyProduct> GetProductsWithNRecentReviews(int howManyReviews)
        {
            List<MyProduct> res = (from product in Products
                where product.ProductReviews.Count == howManyReviews
                select product).ToList();

            return res;
        }
    }
}