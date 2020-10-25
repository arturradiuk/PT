using System;

namespace BookStore
{
    public class CopyDetails
    {
        public Guid Id { get; set; }
        public Book Book { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
        public int Count { get; set; }

        public CopyDetails(Guid id, Book book, decimal price, decimal tax, int count)
        {
            Id = id;
            Book = book;
            Price = price;
            Tax = tax;
            Count = count;
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
                return (this.Id.Equals(cd.Id)) && (this.Book.Equals(cd.Book)) && (this.Price.Equals(cd.Price)) &&
                       (this.Tax.Equals(cd.Tax)) && (this.Count.Equals(cd.Count));
            }
        }
    }
}