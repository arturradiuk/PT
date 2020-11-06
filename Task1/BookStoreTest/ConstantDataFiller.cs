using BookStore;

namespace BookStoreTest
{
    public class ConstantDataFiller:IDataFiller
    {
        public void Fill(DataContext dataContext)
        {
            dataContext.Clients.Add(new Client("jankowalski@mail.com","Jan","Kowalski","000"));
            dataContext.Clients.Add(new Client("jankowalski_1@mail.com","Jan_1","Kowalski","0001"));
            dataContext.Clients.Add(new Client("jankowalski_2@mail.com","Jan_2","Kowalski","0002"));
            dataContext.Clients.Add(new Client("jankowalski_3@mail.com","Jan_3","Kowalski","0003"));
            dataContext.Clients.Add(new Client("jankowalski_4@mail.com","Jan_4","Kowalski","0004"));
            
            
            dataContext.Books.Add(1,new Book("Harry Potter and the Philosopher's Stone_1","Joanne Rowling",1997,Genre.Fiction));
            dataContext.Books.Add(2,new Book("Harry Potter and the Philosopher's Stone_2","Joanne Rowling",1997,Genre.Fiction));
            dataContext.Books.Add(3,new Book("Harry Potter and the Philosopher's Stone_3","Joanne Rowling",1997,Genre.Fiction));
            dataContext.Books.Add(4,new Book("Harry Potter and the Philosopher's Stone_4","Joanne Rowling",1997,Genre.Fiction));
            dataContext.Books.Add(5,new Book("Harry Potter and the Philosopher's Stone_5","Joanne Rowling",1997,Genre.Fiction));
            
            // dataContext.Invoices.Add(new Invoice());
            
        }

    }
}