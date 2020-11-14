using System;
using System.Collections.Generic;
using BookStore.Model;
using BookStore.Model.Entities;
using Xunit;

namespace BookStoreTest
{
    public class ConstantDataFillerTest
    {
        [Fact]
        public void BooksGeneratingTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepository(constantDataFiller);
            IEnumerable<Book> drBooks = dataRepository.GetAllBooks();

            Dictionary<int, Book> books = new Dictionary<int, Book>();
            books.Add(0,
                new Book("Harry Potter and the Philosopher's Stone_0", "Joanne Rowling", 1997));
            books.Add(1,
                new Book("Harry Potter and the Philosopher's Stone_1", "Joanne Rowling", 1997));
            books.Add(2,
                new Book("Harry Potter and the Philosopher's Stone_2", "Joanne Rowling", 1997));
            books.Add(3,
                new Book("Harry Potter and the Philosopher's Stone_3", "Joanne Rowling", 1997));
            books.Add(4,
                new Book("Harry Potter and the Philosopher's Stone_4", "Joanne Rowling", 1997));

            foreach (var b in drBooks)
            {
                Assert.True(books.ContainsValue(b));
            }
        }

        [Fact]
        public void ClientGeneratingTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepository(constantDataFiller);

            List<Client> clients = new List<Client>();
            clients.Add(new Client("jankowalski_0@mail.com", "Jan_0", "Kowalski", "000"));
            clients.Add(new Client("jankowalski_1@mail.com", "Jan_1", "Kowalski", "0001"));
            clients.Add(new Client("jankowalski_2@mail.com", "Jan_2", "Kowalski", "0002"));
            clients.Add(new Client("jankowalski_3@mail.com", "Jan_3", "Kowalski", "0003"));
            clients.Add(new Client("jankowalski_4@mail.com", "Jan_4", "Kowalski", "0004"));

            for (int i = 0; i < clients.Count; i++)
            {
                Assert.True(dataRepository.GetClient(i).Equals(clients[i]));
            }
        }

        [Fact]
        public void CopyDetailsGeneratingTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepository(constantDataFiller);

            Dictionary<int, Book> books = new Dictionary<int, Book>();
            books.Add(0,
                new Book("Harry Potter and the Philosopher's Stone_0", "Joanne Rowling", 1997));
            books.Add(1,
                new Book("Harry Potter and the Philosopher's Stone_1", "Joanne Rowling", 1997));
            books.Add(2,
                new Book("Harry Potter and the Philosopher's Stone_2", "Joanne Rowling", 1997));
            books.Add(3,
                new Book("Harry Potter and the Philosopher's Stone_3", "Joanne Rowling", 1997));
            books.Add(4,
                new Book("Harry Potter and the Philosopher's Stone_4", "Joanne Rowling", 1997));


            List<CopyDetails> copyDetailses = new List<CopyDetails>();
            copyDetailses.Add(new CopyDetails(books[0], 24.5m, 3.4m, 3, "short description 0"));
            copyDetailses.Add(new CopyDetails(books[1], 14.59m, 2.4m, 1, "short description 1"));
            copyDetailses.Add(new CopyDetails(books[2], 26.45m, 1.4m, 3, "short description 2"));
            copyDetailses.Add(new CopyDetails(books[3], 246.99m, 35.4m, 5, "short description 3"));
            copyDetailses.Add(new CopyDetails(books[4], 134.5m, 13.4m, 1, "short description 4"));

            for (int i = 0; i < copyDetailses.Count; i++)
            {
                Assert.True(dataRepository.GetCopyDetails(i).Equals(copyDetailses[i]));
            }
        }

        [Fact]
        public void InvoiceGeneratingTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepository(constantDataFiller);

            List<Client> clients = new List<Client>();
            clients.Add(new Client("jankowalski_0@mail.com", "Jan_0", "Kowalski", "000"));
            clients.Add(new Client("jankowalski_1@mail.com", "Jan_1", "Kowalski", "0001"));
            clients.Add(new Client("jankowalski_2@mail.com", "Jan_2", "Kowalski", "0002"));
            clients.Add(new Client("jankowalski_3@mail.com", "Jan_3", "Kowalski", "0003"));
            clients.Add(new Client("jankowalski_4@mail.com", "Jan_4", "Kowalski", "0004"));

            Dictionary<int, Book> books = new Dictionary<int, Book>();
            books.Add(0,
                new Book("Harry Potter and the Philosopher's Stone_0", "Joanne Rowling", 1997));
            books.Add(1,
                new Book("Harry Potter and the Philosopher's Stone_1", "Joanne Rowling", 1997));
            books.Add(2,
                new Book("Harry Potter and the Philosopher's Stone_2", "Joanne Rowling", 1997));
            books.Add(3,
                new Book("Harry Potter and the Philosopher's Stone_3", "Joanne Rowling", 1997));
            books.Add(4,
                new Book("Harry Potter and the Philosopher's Stone_4", "Joanne Rowling", 1997));

            List<CopyDetails> copyDetailses = new List<CopyDetails>();
            copyDetailses.Add(new CopyDetails(books[0], 24.5m, 3.4m, 3, "short description 0"));
            copyDetailses.Add(new CopyDetails(books[1], 14.59m, 2.4m, 1, "short description 1"));
            copyDetailses.Add(new CopyDetails(books[2], 26.45m, 1.4m, 3, "short description 2"));
            copyDetailses.Add(new CopyDetails(books[3], 246.99m, 35.4m, 5, "short description 3"));
            copyDetailses.Add(new CopyDetails(books[4], 134.5m, 13.4m, 1, "short description 4"));

            

            List<Invoice> invoices = new List<Invoice>();
            invoices.Add(new Invoice(clients[0], copyDetailses[0],
                new DateTime(2006, 4, 14, 2, 34, 44)));
            invoices.Add(new Invoice(clients[1], copyDetailses[1],
                new DateTime(2007, 5, 4, 12, 24, 4)));
            invoices.Add(new Invoice(clients[2], copyDetailses[2],
                new DateTime(2004, 9, 5, 1, 14, 54)));
            invoices.Add(new Invoice(clients[3], copyDetailses[3],
                new DateTime(2008, 10, 10, 8, 39, 33)));
            invoices.Add(new Invoice(clients[4], copyDetailses[4],
                new DateTime(2010, 1, 6, 3, 38, 14)));

            for (int i = 0; i <invoices.Count ; i++)
            {
                Assert.True(invoices[i].Equals(dataRepository.GetInvoice(i)));
            }
        }
    }
}