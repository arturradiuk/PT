using System.Data.Linq;

namespace Task3.MyProduct
{
    public class MyProduct
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public EntitySet<ProductReview> ProductReviews { get; set; }
        public int? ProductSubcategoryID { get; set; }
        public ProductSubcategory ProductSubcategory { get; set; }
        
        private EntitySet<ProductVendor> ProductVendors { get; set; }
        public decimal StandardCost { get; set; }

        public MyProduct(Product product) 
        {
            this.ProductID = product.ProductID;
            this.Name = product.Name;
            this.ProductNumber = product.ProductNumber;
            this.ProductReviews = product.ProductReviews;
            this.ProductVendors = product.ProductVendors;
            this.ProductSubcategoryID = product.ProductSubcategoryID;
            this.ProductSubcategory = product.ProductSubcategory;
            this.StandardCost = product.StandardCost;
        }

        public MyProduct(int productId, string name, string productNumber, int? productSubcategoryId, ProductSubcategory productSubcategory, EntitySet<ProductVendor> productVendors, decimal standardCost)
        {
            ProductID = productId;
            Name = name;
            ProductNumber = productNumber;
            ProductSubcategoryID = productSubcategoryId;
            ProductSubcategory = productSubcategory;
            ProductVendors = productVendors;
            StandardCost = standardCost;
        }
    }
}