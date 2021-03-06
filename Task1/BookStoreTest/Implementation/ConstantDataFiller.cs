using System;
using BookStore.Model;

namespace BookStoreTest
{
    public class ConstantDataFiller : IDataFiller
    {
        public void Fill(DataContext dataContext)
        {
            dataContext.Clients.Add(new Client("jankowalski@mail.com", "Jan_0", "Kowalski", "000"));
            dataContext.Clients.Add(new Client("jankowalski_1@mail.com", "Jan_1", "Kowalski", "0001"));
            dataContext.Clients.Add(new Client("jankowalski_2@mail.com", "Jan_2", "Kowalski", "0002"));
            dataContext.Clients.Add(new Client("jankowalski_3@mail.com", "Jan_3", "Kowalski", "0003"));
            dataContext.Clients.Add(new Client("jankowalski_4@mail.com", "Jan_4", "Kowalski", "0004"));


            dataContext.Books.Add(0,
                new Book("Harry Potter and the Philosopher's Stone_0", "Joanne Rowling", 1997));
            dataContext.Books.Add(1,
                new Book("Harry Potter and the Philosopher's Stone_1", "Joanne Rowling", 1997));
            dataContext.Books.Add(2,
                new Book("Harry Potter and the Philosopher's Stone_2", "Joanne Rowling", 1997));
            dataContext.Books.Add(3,
                new Book("Harry Potter and the Philosopher's Stone_3", "Joanne Rowling", 1997));
            dataContext.Books.Add(4,
                new Book("Harry Potter and the Philosopher's Stone_4", "Joanne Rowling", 1997));


            dataContext.AllCopyDetails.Add(new CopyDetails(dataContext.Books[0], 24.5m, 3.4m, 3,
                "short description 0"));
            dataContext.AllCopyDetails.Add(
                new CopyDetails(dataContext.Books[1], 14.59m, 2.4m, 1, "short description 1"));
            dataContext.AllCopyDetails.Add(
                new CopyDetails(dataContext.Books[2], 26.45m, 1.4m, 3, "short description 2"));
            dataContext.AllCopyDetails.Add(new CopyDetails(dataContext.Books[3], 246.99m, 35.4m, 5,
                "short description 3"));
            dataContext.AllCopyDetails.Add(new CopyDetails(dataContext.Books[4], 134.5m, 13.4m, 1,
                "short description 4"));


            dataContext.Events.Add(new Invoice(dataContext.Clients[0], dataContext.AllCopyDetails[0],
                new DateTime(2006, 4, 14, 2, 34, 44), "short description 0"));
            dataContext.Events.Add(new Invoice(dataContext.Clients[1], dataContext.AllCopyDetails[1],
                new DateTime(2007, 5, 4, 12, 24, 4), "short description 1"));
            dataContext.Events.Add(new Invoice(dataContext.Clients[2], dataContext.AllCopyDetails[2],
                new DateTime(2004, 9, 5, 1, 14, 54), "short description 2"));
            dataContext.Events.Add(new Invoice(dataContext.Clients[3], dataContext.AllCopyDetails[3],
                new DateTime(2008, 10, 10, 8, 39, 33), "short description 3"));
            dataContext.Events.Add(new Invoice(dataContext.Clients[4], dataContext.AllCopyDetails[4],
                new DateTime(2010, 1, 6, 3, 38, 14), "short description 4"));
            dataContext.Events.Add(new Reclamation(new DateTime(2006, 4, 17, 2, 34, 44),
                dataContext.Events[0] as Invoice, "short description for the reclamation"));
        }
    }
}