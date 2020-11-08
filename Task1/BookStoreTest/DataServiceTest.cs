using System;
using System.Collections.Immutable;
using System.Linq;
using BookStore;
using Xunit;

namespace BookStoreTest
{
    public class DataServiceTest
    {
        #region books test

        [Fact]
        public void UpdateBookStockTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);
            CopyDetails copyDetails = dataRepository.GetCopyDetails(0);
            dataService.UpdateBookStock(copyDetails, 1);

            Assert.Equal(1, dataRepository.GetCopyDetails(0).Count);
        }

        [Fact]
        public void BuyBookTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

            Client client = dataRepository.GetClient(2);
            CopyDetails copyDetails = dataRepository.GetCopyDetails(3);
            int originalCopyDetailsCount = copyDetails.Count;
            int totalInvoices = dataRepository.GetAllInvoices().ToImmutableHashSet().Count;
            dataService.BuyBook(client, copyDetails);
            Assert.Equal(originalCopyDetailsCount - 1, dataRepository.GetCopyDetails(3).Count);
            Assert.Equal(totalInvoices + 1, dataRepository.GetAllInvoices().ToImmutableHashSet().Count);
        }

        [Fact]
        public void GetInvoicesForTheBookTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

            Book book = dataRepository.GetBook(1);
            var invoices = dataService.GetInvoicesForTheBook(book);
            foreach (var invoice in invoices)
            {
                Assert.Equal(book, invoice.CopyDetails.Book);
            }
        }

        [Fact]
        public void GetBoughtBooksAndAmountTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

            var boughtBooks = dataService.GetBoughtBooksAndAmount();

            int booksCount = dataService.GetBooks().ToImmutableHashSet().Count;

            Assert.Equal(booksCount, boughtBooks.ToImmutableHashSet().Count);

            int totalInvoices = 0;
            foreach (var book in boughtBooks)
            {
                totalInvoices += book.Item2;
            }

            Assert.Equal(dataService.GetInvoices().ToImmutableHashSet().Count, totalInvoices);
        }

        [Fact]
        public void GetInvoicesForTheBooksAuthorNameTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

            String author = dataRepository.GetBook(0).AuthorName;

            var authorBooks = dataService.GetInvoicesForTheBooksAuthorName(author);

            foreach (var book in authorBooks)
            {
                Assert.Equal(author, book.AuthorName);
            }
        }

        [Fact]
        public void AddBookTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

            Book book = new Book("W pustyni i w puczczy", "Henryk Sienkiewicz", 1910);

            int booksNumber = dataService.GetBooks().ToImmutableHashSet().Count;
            dataService.AddBook(book);
            Assert.Equal(booksNumber + 1, dataService.GetBooks().ToImmutableHashSet().Count);
            Assert.Equal(book, dataService.GetBooks().Last());
        }

        [Fact]
        public void GetBookTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);
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
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

            var allBooks = dataService.GetBooks();

            Assert.Equal(dataRepository.GetAllBooks().ToImmutableHashSet().Count, allBooks.ToImmutableHashSet().Count);
        }

        [Fact]
        public void UpdateBookTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

            Book book = dataRepository.GetBook(0);
            book.BookName = "W pustyni i w puszczy";

            dataService.UpdateBook(book);

            Assert.Equal("W pustyni i w puszczy", dataService.GetBooks().First().BookName);
        }

        [Fact]
        public void DeleteBookTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

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
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

            Client client = dataRepository.GetClient(1);
            var invoices = dataService.GetInvoicesForTheClient(client);
            foreach (var invoice in invoices)
            {
                Assert.Equal(client, invoice.Client);
            }
        }

        [Fact]
        public void GetClientsForTheBookTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

            Book book = dataRepository.GetBook(3);

            var clients = dataService.GetClientsForTheBook(book).ToImmutableHashSet();
            var bookInvoices = dataService.GetInvoicesForTheBook(book);

            foreach (var invoice in bookInvoices)
            {
                Assert.Contains(invoice.Client, clients);
            }
        }

        [Fact]
        public void AddClientTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

            Client client = new Client("michu1212@gmail.com", "Michał", "Kopytko", "8462");

            int clientsNumber = dataService.GetClients().ToImmutableHashSet().Count;
            dataService.AddClient(client);
            Assert.Equal(clientsNumber + 1, dataService.GetClients().ToImmutableHashSet().Count);
            Assert.Equal(client, dataService.GetClients().Last());
        }

        [Fact]
        public void GetClientTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);
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
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

            Client client = dataRepository.GetClient(0);
            client.Email = "my_new_email@gmail.com";

            dataService.UpdateClient(client);

            Assert.Equal("my_new_email@gmail.com", dataService.GetClients().First().Email);
        }

        [Fact]
        public void DeleteClientTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

            Client client = new Client("michu_one_two_three", "Mich", "Kasztanek", "1234");
            dataService.AddClient(client);

            int originalCount = dataService.GetClients().ToImmutableHashSet().Count;

            dataService.DeleteClient(client);

            Assert.Equal(originalCount - 1, dataService.GetClients().ToImmutableHashSet().Count);
            Assert.DoesNotContain(client, dataService.GetClients());
        }

        #endregion

        #region Invoices test region

        [Fact]
        public void GetInvoicesBetweenTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

            DateTime startTime = new DateTime(2005, 1, 1);
            DateTime stopTime = new DateTime(2008, 12, 31);
            var invoices = dataService.GetInvoicesBetween(startTime, stopTime);
            var allInvoices = dataService.GetInvoices();
            foreach (var invoice in allInvoices)
            {
                if (invoice.PurchaseTime >= startTime && invoice.PurchaseTime <= stopTime)
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
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);
            Invoice presentInvoice = dataRepository.GetInvoice(2);
            Client presentInvoiceClient = presentInvoice.Client;
            CopyDetails presentInvoiceCopyDetails = presentInvoice.CopyDetails;
            DateTime presentInvoicePurchaseTime = presentInvoice.PurchaseTime;


            Invoice notPresentInvoice = new Invoice(dataRepository.GetClient(2), dataRepository.GetCopyDetails(3),
                new DateTime(2040, 2, 2));

            Assert.Equal(presentInvoice,
                dataService.GetInvoice(presentInvoiceClient, presentInvoiceCopyDetails, presentInvoicePurchaseTime));
            try
            {
                Assert.NotEqual(notPresentInvoice,
                    dataService.GetInvoice(dataRepository.GetClient(2), dataRepository.GetCopyDetails(3),
                        new DateTime(2040, 2, 2)));
            }
            catch (System.ArgumentException)
            {
            }
        }

        [Fact]
        public void UpdateInvoiceTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

            Invoice invoice = dataRepository.GetInvoice(0);
            DateTime newPurchaseTime = new DateTime(1410, 7, 15);
            invoice.PurchaseTime = newPurchaseTime;

            dataService.UpdateInvoice(invoice);

            Assert.Equal(newPurchaseTime, dataService.GetInvoices().First().PurchaseTime);
        }

        [Fact]
        public void DeleteInvoiceTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

            Invoice invoice = dataRepository.GetInvoice(3);

            int originalCount = dataService.GetInvoices().ToImmutableHashSet().Count;

            dataService.DeleteInvoice(invoice);

            Assert.Equal(originalCount - 1, dataService.GetInvoices().ToImmutableHashSet().Count);
            Assert.DoesNotContain(invoice, dataService.GetInvoices());
        }

        #endregion

        #region copyDetails Tests

        [Fact]
        public void AddCopyDetailsTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

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
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);
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
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

            CopyDetails copyDetails = dataRepository.GetCopyDetails(0);
            copyDetails.Count = 999;

            dataService.UpdateCopyDetails(copyDetails);

            Assert.Equal(999, dataService.GetCopyDetailses().First().Count);
        }

        [Fact]
        public void DeleteCopyDetailsTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

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