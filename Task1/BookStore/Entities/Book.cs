using System;

namespace BookStore
{
    public class Book
    {
        public Guid Id { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int Year { get; set; }
        public Genre Genre { get; set; }
    }
    
    public enum Genre
    {
        
    }
}