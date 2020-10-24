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
    }
}