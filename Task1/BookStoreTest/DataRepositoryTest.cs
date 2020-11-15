using System;
using System.Linq;
using BookStore;
using Xunit;

namespace BookStoreTest
{
    public class DataRepositoryTest
    {
        // todo should we test the implemented fillers
        // todo does we should here implemented fillers

         #region client test region

        [Fact]
        public void GetAllClientsTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Assert.Equal(5, dataRepository.GetAllClients().Count());
        }

        [Fact]
        public void AddClientValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            dataRepository.AddClient(client);

            Assert.Equal(6, dataRepository.GetAllClients().Count());
            Assert.Equal(client, dataRepository.GetClient(5));
        }

        [Fact]
        public void AddClientInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            dataRepository.AddClient(client);

            Assert.Throws<ArgumentException>(() => dataRepository.AddClient(client));
        }

        [Fact]
        public void FindClientValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            dataRepository.AddClient(client);
            Assert.StrictEqual(5, dataRepository.FindClient(client));
        }

        [Fact]
        public void FindClientInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            Assert.Throws<ArgumentException>(() => dataRepository.FindClient(client) as object);
        }

        [Fact]
        public void UpdateClientValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);
            dataRepository.AddClient(client);

            Assert.Equal("Lolek", dataRepository.GetClient(5).FirstName);
            client.FirstName = "Tola";
            dataRepository.UpdateClient(client, 5);
            Assert.Equal("Tola", dataRepository.GetClient(5).FirstName);
        }

        [Fact]
        public void UpdateClientInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);
            dataRepository.AddClient(client);

            Assert.Equal("Lolek", dataRepository.GetClient(5).FirstName);
            client.FirstName = "Tola";

            Assert.Throws<ArgumentException>(() => dataRepository.UpdateClient(client, 105));
        }

        [Fact]
        public void DeleteClientValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);
            dataRepository.AddClient(client);

            Assert.Equal(6, dataRepository.GetAllClients().Count());
            dataRepository.DeleteClient(client);
            Assert.Equal(5, dataRepository.GetAllClients().Count());
        }

        [Fact]
        public void DeleteClientInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            Assert.Equal(5, dataRepository.GetAllClients().Count());
            Assert.Throws<ArgumentException>(() => dataRepository.DeleteClient(client));
        }

        [Fact]
        public void DeleteClientForbiddenTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            Client client = dataRepository.GetEvent(0).Client;

            Assert.Throws<ArgumentException>(() => dataRepository.DeleteClient(client));
        }

        [Fact]
        public void GetClientValidValue()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);
            dataRepository.AddClient(client);
            Assert.NotNull(dataRepository.GetClient(dataRepository.FindClient(client)));
        }

        [Fact]
        public void GetClientInvalidValue()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            Assert.Throws<ArgumentException>(() => dataRepository.GetClient(dataRepository.FindClient(client)));
        }

        #endregion

        #region invoice test region

        [Fact]
        public void GetAllInvoicesTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Assert.Equal(5, dataRepository.GetAllEvents().Count());
        }

        [Fact]
        public void AddInvoiceValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            dataRepository.AddClient(client);

            Assert.Equal(6, dataRepository.GetAllClients().Count());
            Assert.Equal(client, dataRepository.GetClient(5));

            Invoice invoice = new Invoice(client, dataRepository.GetCopyDetails(0), DateTime.Now);
            dataRepository.AddEvent(invoice);
            Assert.Equal(6, dataRepository.GetAllEvents().Count());
            Assert.Equal(invoice, dataRepository.GetEvent(5));
        }

        [Fact]
        public void AddInvoiceInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            dataRepository.AddClient(client);

            Assert.Equal(6, dataRepository.GetAllClients().Count());
            Assert.Equal(client, dataRepository.GetClient(5));

            Invoice invoice = new Invoice(client, dataRepository.GetCopyDetails(0), DateTime.Now);
            dataRepository.AddEvent(invoice);

            Assert.Equal(6, dataRepository.GetAllEvents().Count());
            Assert.Equal(invoice, dataRepository.GetEvent(5));
            Assert.Throws<ArgumentException>(() => dataRepository.AddEvent(invoice));
        }

        [Fact]
        public void FindInvoiceValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            dataRepository.AddClient(client);

            Assert.Equal(6, dataRepository.GetAllClients().Count());
            Assert.Equal(client, dataRepository.GetClient(5));

            Invoice invoice = new Invoice(client, dataRepository.GetCopyDetails(0), DateTime.Now);
            dataRepository.AddEvent(invoice);

            Assert.Equal(5, dataRepository.FindEvent(invoice));
        }

        [Fact]
        public void FindInvoiceInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            dataRepository.AddClient(client);

            Assert.Equal(6, dataRepository.GetAllClients().Count());
            Assert.Equal(client, dataRepository.GetClient(5));

            Invoice invoice = new Invoice(client, dataRepository.GetCopyDetails(0), DateTime.Now);

            Assert.Throws<ArgumentException>(() => dataRepository.FindEvent(invoice) as object);
        }

        [Fact]
        public void UpdateInvoiceValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            dataRepository.AddClient(client);


            Invoice invoice = new Invoice(client, dataRepository.GetCopyDetails(0), new DateTime(2001, 10, 5));
            dataRepository.AddEvent(invoice);
            Assert.Equal(new DateTime(2001, 10, 5), dataRepository.GetEvent(5).EventDateTime);
        }

        [Fact]
        public void UpdateInvoiceInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            dataRepository.AddClient(client);


            Invoice invoice = new Invoice(client, dataRepository.GetCopyDetails(0), new DateTime(2001, 10, 5));
            dataRepository.AddEvent(invoice);
            Assert.Throws<ArgumentException>(() => dataRepository.GetEvent(504).EventDateTime as object);
        }

        [Fact]
        public void DeleteInvoiceValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            dataRepository.AddClient(client);


            Invoice invoice = new Invoice(client, dataRepository.GetCopyDetails(0), new DateTime(2001, 10, 5));
            dataRepository.AddEvent(invoice);

            Assert.Equal(6, dataRepository.GetAllEvents().Count());
            dataRepository.DeleteEvent(invoice);
            Assert.Equal(5, dataRepository.GetAllEvents().Count());
        }

        [Fact]
        public void DeleteInvoiceInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            dataRepository.AddClient(client);


            Invoice invoice = new Invoice(client, dataRepository.GetCopyDetails(0), new DateTime(2001, 10, 5));

            Assert.Equal(5, dataRepository.GetAllEvents().Count());
            Assert.Throws<ArgumentException>(() => dataRepository.DeleteEvent(invoice));
        }

        [Fact]
        public void GetInvoiceValidValue()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            dataRepository.AddClient(client);


            Invoice invoice = new Invoice(client, dataRepository.GetCopyDetails(0), new DateTime(2001, 10, 5));
            dataRepository.AddEvent(invoice);

            Assert.NotNull(dataRepository.GetEvent(dataRepository.FindEvent(invoice)));
        }

        [Fact]
        public void GetInvoiceInvalidValue()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            dataRepository.AddClient(client);


            Invoice invoice = new Invoice(client, dataRepository.GetCopyDetails(0), new DateTime(2001, 10, 5));


            Assert.Throws<ArgumentException>(() => dataRepository.GetEvent(dataRepository.FindEvent(invoice)));
        }

        #endregion

        #region book test region

        [Fact]
        public void GetAllBooksTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Assert.Equal(5, dataRepository.GetAllBooks().Count());
        }

        [Fact]
        public void AddBookValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Book book = new Book("Year 1984", "George Orwell", 1949);

            dataRepository.AddBook(book);

            Assert.Equal(6, dataRepository.GetAllBooks().Count());
            Assert.Equal(book, dataRepository.GetBook(6));
        }

        [Fact]
        public void AddBookInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Book book = new Book("Year 1984", "George Orwell", 1949);

            dataRepository.AddBook(book);

            Assert.Equal(6, dataRepository.GetAllBooks().Count());
            Assert.Equal(book, dataRepository.GetBook(6));


            Assert.Throws<ArgumentException>(() => dataRepository.AddBook(book));
        }

        [Fact]
        public void FindBookValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Book book = new Book("Year 1984", "George Orwell", 1949);

            dataRepository.AddBook(book);
            Assert.StrictEqual(6, dataRepository.FindBook(book));
        }

        [Fact]
        public void FindBookInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Book book = new Book("Year 1984", "George Orwell", 1949);

            Assert.Throws<ArgumentException>(() => dataRepository.FindBook(book) as object);
        }

        [Fact]
        public void UpdateBookValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Book book = new Book("Year 1984", "George Orwell", 1949);

            dataRepository.AddBook(book);

            Assert.Equal("George Orwell", dataRepository.GetBook(6).AuthorName);
            book.AuthorName = "Tola";
            dataRepository.UpdateBook(book, 6);
            Assert.Equal("Tola", dataRepository.GetBook(6).AuthorName);
        }

        [Fact]
        public void UpdateBookInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Book book = new Book("Year 1984", "George Orwell", 1949);

            dataRepository.AddBook(book);

            Assert.Equal("George Orwell", dataRepository.GetBook(6).AuthorName);
            book.AuthorName = "Tola";

            Assert.Throws<ArgumentException>(() => dataRepository.UpdateBook(book, 105));
        }

        [Fact]
        public void DeleteBookValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            Book book = new Book("Year 1984", "George Orwell", 1949);
            dataRepository.AddBook(book);
            Assert.Equal(6, dataRepository.GetAllBooks().Count());
            dataRepository.DeleteBook(book);
            Assert.Equal(5, dataRepository.GetAllBooks().Count());
        }

        [Fact]
        public void DeleteBookInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            Book book = new Book("Year 1984", "George Orwell", 1949);

            Assert.Equal(5, dataRepository.GetAllBooks().Count());
            Assert.Throws<ArgumentException>(() => dataRepository.DeleteBook(book));
        }

        [Fact]
        public void DeleteBookForbiddenTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            Book book = dataRepository.GetEvent(0).CopyDetails.Book;

            Assert.Throws<ArgumentException>(() => dataRepository.DeleteBook(book));
        }

        [Fact]
        public void GetBookValidValue()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            Book book = new Book("Year 1984", "George Orwell", 1949);
            dataRepository.AddBook(book);

            Assert.NotNull(dataRepository.GetBook(dataRepository.FindBook(book)));
        }

        [Fact]
        public void GetBookInvalidValue()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            Book book = new Book("Year 1984", "George Orwell", 1949);

            Assert.Throws<ArgumentException>(() => dataRepository.GetBook(dataRepository.FindBook(book)));
        }

        #endregion

        #region copydetails test region

        [Fact]
        public void GetAllCopyDetailsesTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Assert.Equal(5, dataRepository.GetAllCopyDetailses().Count());
        }

        [Fact]
        public void AddCopyDetailsValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Book b1 = new Book("Year 1984", "George Orwell", 1949);

            CopyDetails cd1 = new CopyDetails(b1, new decimal(35.99), new decimal(2.65), 1, "short description");

            dataRepository.AddCopyDetails(cd1);

            Assert.Equal(6, dataRepository.GetAllCopyDetailses().Count());
            Assert.Equal(cd1, dataRepository.GetCopyDetails(5));
        }

        [Fact]
        public void AddCopyDetailsInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Book b1 = new Book("Year 1984", "George Orwell", 1949);

            CopyDetails cd1 = new CopyDetails(b1, new decimal(35.99), new decimal(2.65), 1, "short description");

            dataRepository.AddCopyDetails(cd1);


            Assert.Throws<ArgumentException>(() => dataRepository.AddCopyDetails(cd1));
        }

        [Fact]
        public void FindClientCopyDetailsValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Book b1 = new Book("Year 1984", "George Orwell", 1949);

            CopyDetails cd1 = new CopyDetails(b1, new decimal(35.99), new decimal(2.65), 1, "short description");

            dataRepository.AddCopyDetails(cd1);

            Assert.StrictEqual(5, dataRepository.FindCopyDetails(cd1));
        }

        [Fact]
        public void FindCopyDetailsInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Book b1 = new Book("Year 1984", "George Orwell", 1949);

            CopyDetails cd1 = new CopyDetails(b1, new decimal(35.99), new decimal(2.65), 1, "short description");

            Assert.Throws<ArgumentException>(() => (dataRepository.FindCopyDetails(cd1)) as object);
        }

        [Fact]
        public void UpdateCopyDetailsValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Book b1 = new Book("Year 1984", "George Orwell", 1949);

            CopyDetails cd1 = new CopyDetails(b1, new decimal(35.99), new decimal(2.65), 1, "short description");

            dataRepository.AddCopyDetails(cd1);

            Assert.Equal("short description", dataRepository.GetCopyDetails(5).Description);
            cd1.Description = "long description";
            dataRepository.UpdateCopyDetails(cd1, 5);
            Assert.Equal("long description", dataRepository.GetCopyDetails(5).Description);
        }

        [Fact]
        public void UpdateCopyDetailsInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Book b1 = new Book("Year 1984", "George Orwell", 1949);

            CopyDetails cd1 = new CopyDetails(b1, new decimal(35.99), new decimal(2.65), 1, "short description");

            dataRepository.AddCopyDetails(cd1);

            Assert.Equal("short description", dataRepository.GetCopyDetails(5).Description);
            cd1.Description = "long description";

            Assert.Throws<ArgumentException>(() => dataRepository.UpdateCopyDetails(cd1, 105));
        }

        [Fact]
        public void DeleteCopyDetailsValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Book b1 = new Book("Year 1984", "George Orwell", 1949);

            CopyDetails cd1 = new CopyDetails(b1, new decimal(35.99), new decimal(2.65), 1, "short description");

            dataRepository.AddCopyDetails(cd1);
            
            Assert.Equal(6, dataRepository.GetAllCopyDetailses().Count());
            dataRepository.DeleteCopyDetails(cd1);
            Assert.Equal(5, dataRepository.GetAllCopyDetailses().Count());
        }

        [Fact]
        public void DeleteCopyDetailsInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Book b1 = new Book("Year 1984", "George Orwell", 1949);

            CopyDetails cd1 = new CopyDetails(b1, new decimal(35.99), new decimal(2.65), 1,"short description");
            

            Assert.Equal(5, dataRepository.GetAllCopyDetailses().Count());
            Assert.Throws<ArgumentException>(() => dataRepository.DeleteCopyDetails(cd1));
        }

        [Fact]
        public void DeleteCopyDetailsForbiddenTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            CopyDetails copyDetails = dataRepository.GetEvent(0).CopyDetails;

            Assert.Throws<ArgumentException>(() => dataRepository.DeleteCopyDetails(copyDetails));
        }

        [Fact]
        public void GetCopyDetailsValidValue()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Book b1 = new Book("Year 1984", "George Orwell", 1949);

            CopyDetails cd1 = new CopyDetails(b1, new decimal(35.99), new decimal(2.65), 1,"short description");
            
            dataRepository.AddCopyDetails(cd1);
            
            Assert.NotNull(dataRepository.GetCopyDetails(dataRepository.FindCopyDetails(cd1)));
        }

        [Fact]
        public void GetCopyDetailsInvalidValue()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Book b1 = new Book("Year 1984", "George Orwell", 1949);

            CopyDetails cd1 = new CopyDetails(b1, new decimal(35.99), new decimal(2.65), 1,"short description");
            

            Assert.Throws<ArgumentException>(() => dataRepository.GetCopyDetails(dataRepository.FindCopyDetails(cd1)));
        }

        #endregion
    }
}