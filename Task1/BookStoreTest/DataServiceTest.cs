using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BookStore;
using BookStore.Logic;
using BookStore.Model;
using BookStore.Model.Entities;
using BookStoreTest.Implementation;
using Xunit;

namespace BookStoreTest
{
    // todo remove all var 
    public class DataServiceTest
    {
        #region books test

        [Fact]
        public void UpdateBookStockTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);
            CopyDetails copyDetails = dataRepository.GetCopyDetails(0);
            dataService.UpdateBookStock(copyDetails, 1);

            Assert.Equal(1, dataRepository.GetCopyDetails(0).Count);
        }

        [Fact]
        public void BuyBookTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);

            Client client = dataRepository.GetClient(2);
            CopyDetails copyDetails = dataRepository.GetCopyDetails(3);
            int originalCopyDetailsCount = copyDetails.Count;
            int totalInvoices = dataRepository.GetAllEvents().ToImmutableHashSet().Count;
            dataService.BuyBook(client, copyDetails, "description");
            Assert.Equal(originalCopyDetailsCount - 1, dataRepository.GetCopyDetails(3).Count);
            Assert.Equal(totalInvoices + 1, dataRepository.GetAllEvents().ToImmutableHashSet().Count);
        }

        [Fact]
        void ReturnBookTest()
        {
            // throw new IncompleteInitialization();
        }

        [Fact]
        public void GetInvoicesForTheBookTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);

            Book book = dataRepository.GetBook(1);
            IEnumerable<Event> eEvent = dataService.GetInvoicesForTheBook(book);
            foreach (var e in eEvent)
            {
                Reclamation r = e as Reclamation;
                Invoice i = e as Invoice;
                if (i != null)
                {
                    Assert.Equal(book, i.CopyDetails.Book);
                }
                else
                {
                    Assert.Equal(book, r.Invoice.CopyDetails.Book);
                }
            }
        }

        [Fact]
        public void GetBoughtBooksAndAmountTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);

            var boughtBooks = dataService.GetBoughtBooksAndAmount();

            int booksCount = dataService.GetBooks().ToImmutableHashSet().Count;

            Assert.Equal(booksCount, boughtBooks.ToImmutableHashSet().Count);

            int totalInvoices = 0;
            foreach (var book in boughtBooks)
            {
                totalInvoices += book.Item2;
            }

            Assert.Equal(dataService.GetEvents().ToImmutableHashSet().Count, totalInvoices);
        }

        // [Fact]
        // public void GetInvoicesForTheBooksAuthorNameTest()
        // {
        // IDataFiller constantDataFiller = new ConstantDataFiller();
        // IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
        // IDataService dataService = new DataService(dataRepository);

        // String author = dataRepository.GetBook(0).AuthorName;

        // var authorBooks = dataService.GetEventForTheBooksAuthorName(author);

        // foreach (var book in authorBooks)
        // {
        // Assert.Equal(author, book.AuthorName);
        // }
        // }

        [Fact]
        public void AddBookTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);

            Book book = new Book("W pustyni i w puczczy", "Henryk Sienkiewicz", 1910);

            int booksNumber = dataService.GetBooks().ToImmutableHashSet().Count;
            dataService.AddBook(book);
            Assert.Equal(booksNumber + 1, dataService.GetBooks().ToImmutableHashSet().Count);
            Assert.Equal(book, dataService.GetBooks().Last());
        }

        [Fact]
        public void GetBookTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);
            Book presentBook = dataRepository.GetBook(0);
            String presentBookName = presentBook.BookName;
            String presentBookAuthor = presentBook.AuthorName;
            int presentBookYear = presentBook.Year;

            // Book x = new Book(presentBookName, presentBookAuthor, presentBookYear);
            // Assert.Equal(x, presentBook);

            Book notPresentBook = new Book("I don't exist", "Neither do I", 2999);

            Assert.Equal(presentBook, dataService.GetBook(presentBookName, presentBookAuthor, presentBookYear));
            try
            {
                Assert.NotEqual(notPresentBook, dataService.GetBook("I don't exist", "Neither do I", 2999));
            }
            catch (System.ArgumentException)
            {
            }
        }

        [Fact]
        public void GetBooksTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);

            var allBooks = dataService.GetBooks();

            Assert.Equal(dataRepository.GetAllBooks().ToImmutableHashSet().Count, allBooks.ToImmutableHashSet().Count);
        }

        [Fact]
        public void UpdateBookTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);

            Book book = dataRepository.GetBook(0);
            book.BookName = "W pustyni i w puszczy";

            dataService.UpdateBook(book);

            Assert.Equal("W pustyni i w puszczy", dataService.GetBooks().First().BookName);
        }

        [Fact]
        public void DeleteBookTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);

            Book book = new Book("W pustyni i w puczczy", "Henryk Sienkiewicz", 1910);
            dataService.AddBook(book);

            int originalCount = dataService.GetBooks().ToImmutableHashSet().Count;

            dataService.DeleteBook(book);

            Assert.Equal(originalCount - 1, dataService.GetBooks().ToImmutableHashSet().Count);
            Assert.DoesNotContain(book, dataService.GetBooks());
        }

        #endregion

        #region client tests

        [Fact]
        public void GetInvoicesForTheClientTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);

            Client client = dataRepository.GetClient(1);
            IEnumerable<Event> events = dataService.GetInvoicesForTheClient(client);
            foreach (Event e in events)
            {
                Reclamation r = e as Reclamation;
                Invoice i = e as Invoice;
                if (i != null)
                {
                    Assert.Equal(client, i.Client);
                }
                else
                {
                    Assert.Equal(client, r.Invoice.Client);
                }
            }
        }

        [Fact]
        public void GetClientsForTheBookTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);

            Book book = dataRepository.GetBook(3);

            var clients = dataService.GetClientsForTheBook(book).ToImmutableHashSet();
            IEnumerable<Event> events = dataService.GetInvoicesForTheBook(book);

            foreach (Event e in events)
            {
                Reclamation r = e as Reclamation;
                Invoice i = e as Invoice;
                if (i != null)
                {
                    Assert.Contains(i.Client, clients);
                }
                else
                {
                    Assert.Contains(r.Invoice.Client, clients);
                }
            }
        }

        [Fact]
        public void AddClientTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);

            Client client = new Client("michu1212@gmail.com", "Michał", "Kopytko", "8462");

            int clientsNumber = dataService.GetClients().ToImmutableHashSet().Count;
            dataService.AddClient(client);
            Assert.Equal(clientsNumber + 1, dataService.GetClients().ToImmutableHashSet().Count);
            Assert.Equal(client, dataService.GetClients().Last());
        }

        [Fact]
        public void GetClientTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);
            Client presentClient = dataRepository.GetClient(2);
            String presentClientEmail = presentClient.Email;
            String presentClientFirstName = presentClient.FirstName;
            String presentClientSecondName = presentClient.SecondName;
            String presentClientPhoneNumber = presentClient.PhoneNumber;

            Client notPresentClient = new Client("michu_one_two_three", "Mich", "Kasztanek", "1234");

            Assert.Equal(presentClient,
                dataService.GetClient(presentClientEmail, presentClientFirstName, presentClientSecondName,
                    presentClientPhoneNumber));
            try
            {
                Assert.NotEqual(notPresentClient,
                    dataService.GetClient("michu_one_two_three", "Mich", "Kasztanek", "1234"));
            }
            catch (System.ArgumentException)
            {
            }
        }

        [Fact]
        public void UpdateClientTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);

            Client client = dataRepository.GetClient(0);
            client.Email = "my_new_email@gmail.com";

            dataService.UpdateClient(client);

            Assert.Equal("my_new_email@gmail.com", dataService.GetClients().First().Email);
        }

        [Fact]
        public void DeleteClientTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);

            Client client = new Client("michu_one_two_three", "Mich", "Kasztanek", "1234");
            dataService.AddClient(client);

            int originalCount = dataService.GetClients().ToImmutableHashSet().Count;

            dataService.DeleteClient(client);

            Assert.Equal(originalCount - 1, dataService.GetClients().ToImmutableHashSet().Count);
            Assert.DoesNotContain(client, dataService.GetClients());
        }

        #endregion

        #region Events test region

        [Fact]
        public void GetInvoicesBetweenTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);

            DateTime startTime = new DateTime(2005, 1, 1);
            DateTime stopTime = new DateTime(2008, 12, 31);
            var invoices = dataService.GetEventsBetween(startTime, stopTime); // todo remove var
            var allInvoices = dataService.GetEvents();
            foreach (var invoice in allInvoices)
            {
                if (invoice.EventDateTime >= startTime && invoice.EventDateTime <= stopTime)
                {
                    Assert.Contains(invoice, invoices);
                }

                else
                {
                    Assert.DoesNotContain(invoice, invoices);
                }
            }
        }

        [Fact]
        public void GetInvoiceTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);
            Invoice presentInvoice = dataRepository.GetEvent(2) as Invoice;
            Client presentInvoiceClient = presentInvoice.Client;
            CopyDetails presentInvoiceCopyDetails = presentInvoice.CopyDetails;
            DateTime presentInvoicePurchaseTime = presentInvoice.EventDateTime;


            Invoice notPresentInvoice = new Invoice(dataRepository.GetClient(2), dataRepository.GetCopyDetails(3),
                new DateTime(2040, 2, 2), "description");

            Assert.Equal(presentInvoice,
                dataService.GetInvoice(presentInvoiceClient, presentInvoiceCopyDetails, presentInvoicePurchaseTime,
                    "description"));
            try
            {
                Assert.NotEqual(notPresentInvoice,
                    dataService.GetInvoice(dataRepository.GetClient(2), dataRepository.GetCopyDetails(3),
                        new DateTime(2040, 2, 2), "description"));
            }
            catch (System.ArgumentException)
            {
            }
        }

        [Fact]
        public void UpdateInvoiceTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);

            Invoice invoice = (Invoice) dataRepository.GetEvent(0);
            DateTime newPurchaseTime = new DateTime(1410, 7, 15);
            invoice.EventDateTime = newPurchaseTime;

            dataService.UpdateEvent(invoice);

            Assert.Equal(newPurchaseTime, dataService.GetEvents().First().EventDateTime);
        }

        [Fact]
        public void DeleteInvoiceTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);

            Invoice invoice = (Invoice) dataRepository.GetEvent(3);

            int originalCount = dataService.GetEvents().ToImmutableHashSet().Count;

            dataService.DeleteEvent(invoice);

            Assert.Equal(originalCount - 1, dataService.GetEvents().ToImmutableHashSet().Count);
            Assert.DoesNotContain(invoice, dataService.GetEvents());
        }

        #endregion

        #region copyDetails Tests

        [Fact]
        public void AddCopyDetailsTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);

            CopyDetails copyDetails =
                new CopyDetails(dataRepository.GetBook(3), 15.60m, 2.60m, 2, "Sample book invoice");
            int copyDetailsNumber = dataService.GetCopyDetailses().ToImmutableHashSet().Count;

            dataService.AddCopyDetails(copyDetails);
            Assert.Equal(copyDetailsNumber + 1, dataService.GetCopyDetailses().ToImmutableHashSet().Count);
            Assert.Equal(copyDetails, dataService.GetCopyDetailses().Last());
        }

        [Fact]
        public void GetCopyDetailsTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);
            CopyDetails presentCopyDetails = dataRepository.GetCopyDetails(3);
            Book book = presentCopyDetails.Book;
            decimal price = presentCopyDetails.Price;
            decimal tax = presentCopyDetails.Tax;
            int count = presentCopyDetails.Count;
            String description = presentCopyDetails.Description;

            CopyDetails notPresentCopyDetails =
                new CopyDetails(book, price, tax, 50, "There is no such description");
            Assert.Equal(presentCopyDetails,
                dataService.GetCopyDetails(book, price, tax, count, description));
            Assert.DoesNotContain(notPresentCopyDetails, dataService.GetCopyDetailses());
        }

        [Fact]
        public void UpdateCopyDetailsTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);

            CopyDetails copyDetails = dataRepository.GetCopyDetails(0);
            copyDetails.Count = 999;

            dataService.UpdateCopyDetails(copyDetails);

            Assert.Equal(999, dataService.GetCopyDetailses().First().Count);
        }

        [Fact]
        public void DeleteCopyDetailsTest()
        {
            IDataFiller constantDataFiller = new ConstantDataFiller();
            IDataRepository dataRepository = new DataRepositoryForTest(constantDataFiller);
            IDataService dataService = new DataService(dataRepository);

            CopyDetails copyDetails = new CopyDetails(dataRepository.GetBook(2), 15.6m, 2.30m, 1, "Sample invoice");
            dataService.AddCopyDetails(copyDetails);

            int originalCount = dataService.GetCopyDetailses().ToImmutableHashSet().Count;

            dataService.DeleteCopyDetails(copyDetails);

            Assert.Equal(originalCount - 1, dataService.GetCopyDetailses().ToImmutableHashSet().Count);
            Assert.DoesNotContain(copyDetails, dataService.GetCopyDetailses());
        }

        #endregion
    }
}