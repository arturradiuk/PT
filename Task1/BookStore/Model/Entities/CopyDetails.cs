namespace BookStore.Model.Entities
{
    public class CopyDetails
    {
        public Book Book { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
        public int Count { get; set; }

        public string Description { get; set; }

        public CopyDetails(Book book, decimal price, decimal tax, int count, string description)
        {
            Book = book;
            Price = price;
            Tax = tax;
            Count = count;
            Description = description;
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                CopyDetails cd = (CopyDetails) obj;
                return (this.Book.Equals(cd.Book)) && (this.Price.Equals(cd.Price)) &&
                       (this.Tax.Equals(cd.Tax)) && (this.Count.Equals(cd.Count)) &&
                       (this.Description.Equals(cd.Description));
            }
        }

        public override string ToString()
        {
            return "CopyDetails: " + Book.ToString() + "Price = " + Price + "; Tax = " + Tax + "; Count = " + Count +
                   "; Description = " + Description + "; ";
        }
    }
}