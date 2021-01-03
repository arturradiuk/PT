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
    }
}