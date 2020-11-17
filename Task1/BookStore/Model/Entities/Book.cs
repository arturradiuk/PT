namespace BookStore.Model
{
    public class Book
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int Year { get; set; }


        public Book(string bookName, string authorName, int year)
        {
            BookName = bookName;
            AuthorName = authorName;
            Year = year;
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
                       (this.AuthorName.Equals(b.AuthorName)) && (this.Year.Equals(b.Year));
            }
        }

        public override string ToString()
        {
            return "Book: BookName = " + BookName + "; AuthorName = " + AuthorName + "; Year = " + Year + "; ";
        }
    }
}