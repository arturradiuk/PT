using System;

namespace BookStore
{
    public class Book
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int Year { get; set; }
        public Genre Genre { get; set; }
        
        
        public Book(string bookName, string authorName, int year, Genre genre)
        {
            BookName = bookName;
            AuthorName = authorName;
            Year = year;
            Genre = genre;
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Book b = (Book) obj;
                return (this.BookName.Equals(b.BookName)) &&
                       (this.AuthorName.Equals(b.AuthorName)) && (this.Year.Equals(b.Year)) &&
                       (this.Genre.Equals(b.Genre));
            }
        }
    }

    public enum Genre
    {
        Biography,
        Fiction,
        Classic,
        Horror
    }
}