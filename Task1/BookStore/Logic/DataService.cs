using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


// todo przenieść implementacje IDataFiller do testów ~~~~ SJ
// todo event - zachowanie polimorficzne ---> model(entities, datarepository, dataservice); testy;  ~~~~~ AR
// todo w testach umieścić implementacje IDataRepository na potrzeby testów ~~~~~ AR 
// todo stworzyć api dla warstwy logiki IDataService ~~~~~ SJ
// todo dodać przestrzeni nazw dla warstw ~~~~~ SJ

// todo create some additional tests
// todo modify existing tests 

// todo add some Reclamations to the Fillers 
// todo test returnBook method

// todo write some tests for the invalid operations 
// todo create test for the invalid reclamation deleting in dataRepository test


namespace BookStore
{
    public class DataService
    {
        private IDataRepository _dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public void
            UpdateBookStock(CopyDetails copyDetails, int count)
        {
            if (count < 0)
            {
                throw new InvalidOperationException("You can't update book stock with negative number.");
            }

            copyDetails.Count = count;
            _dataRepository.UpdateCopyDetails(copyDetails, _dataRepository.FindCopyDetails(copyDetails));
        }

        public Invoice BuyBook(Client client, CopyDetails copyDetails)
        {
            if (copyDetails.Count <= 0)
            {
                throw new InvalidOperationException("We don't have these books, wait for book stock updating.");
            }

            Invoice createdInvoice = new Invoice(client, copyDetails, DateTime.Now);
            copyDetails.Count -= 1;
            _dataRepository.UpdateCopyDetails(copyDetails, this._dataRepository.FindCopyDetails(copyDetails));
            _dataRepository.AddEvent(createdInvoice);
            return createdInvoice;
        }

        public Reclamation ReturnBook(Client client, CopyDetails copyDetails, Invoice invoice) 
        {
            if (!this._dataRepository.GetAllEvents().Contains(invoice))
            {
                throw new InvalidOperationException("We don't have such invoice.");
            }

            Reclamation reclamation = new Reclamation(client, copyDetails, DateTime.Now,invoice);
            copyDetails.Count += 1;
            _dataRepository.UpdateCopyDetails(copyDetails, this._dataRepository.FindCopyDetails(copyDetails));
            _dataRepository.AddEvent(reclamation);
            return reclamation;
        }

        public IEnumerable<Event> GetEventsForTheBook(Book book)
        {
            return GetEvents().Where(eEvent => eEvent.CopyDetails.Book.Equals(book));
        }

        public IEnumerable<ValueTuple<Book, int>> GetBoughtBooksAndAmount()
        {
            List<ValueTuple<Book, int>> temp = new List<(Book, int)>();
            IEnumerable<Book> books = GetBooks();

            foreach (var b in books)
            {
                int amount = GetEventsForTheBook(b).Count();
                if (amount != 0)
                {
                    temp.Add((b, amount));
                }
            }

            return temp;
        }

        public IEnumerable<Event> GetEventsForTheClient(Client client)
        {
            return GetEvents().Where(eEvent => eEvent.Client.Email.Equals(client.Email));
        }


        public IEnumerable<Book> GetEventForTheBooksAuthorName(string authorName)
        {
            return GetBooks().Where(book => book.AuthorName.Equals(authorName));
        }

        public IEnumerable<Event> GetEventsBetween(DateTime start, DateTime end) // todo check 
        {
            return GetEvents().Where(eEvent =>
                (eEvent.EventDateTime.CompareTo(start) == 1) && (eEvent.EventDateTime.CompareTo(end) == -1));
        }

        public IEnumerable<Client> GetClientsForTheBook(Book book)
        {
            IEnumerable<Event> eEvent = GetEventsForTheBook(book);
            List<Client> clients = new List<Client>();
            foreach (var i in eEvent)
            {
                clients.Add(i.Client);
            }

            return clients;
        }

        public void AddBook(Book book)
        {
            _dataRepository.AddBook(book);
        }

        public void AddClient(Client client)
        {
            _dataRepository.AddClient(client);
        }

        public void AddCopyDetails(CopyDetails copyDetails)
        {
            _dataRepository.AddCopyDetails(copyDetails);
        }


        public Book GetBook(string bookName, string author, int year)
        {
            return _dataRepository.GetBook(_dataRepository.FindBook(new Book(bookName, author, year)));
        }

        public Client GetClient(string email, string firstName, string secondName, string phoneNumber)
        {
            return _dataRepository.GetClient(
                _dataRepository.FindClient(new Client(email, firstName, secondName, phoneNumber)));
        }

        // public Event GetEvent(Client client, CopyDetails copyDetails, DateTime eventDateTime) // todo remove getEvent and create GetInvoice + GetReclamation
        // {
        //     return _dataRepository.GetEvent(
        //         _dataRepository.FindEvent(new Invoice(client, copyDetails, eventDateTime)));
        // }

        public Invoice GetInvoice(Client client, CopyDetails copyDetails, DateTime eventDateTime)
        {
            return _dataRepository.GetEvent(
                _dataRepository.FindEvent(new Invoice(client, copyDetails, eventDateTime))) as Invoice;
        }

        public Reclamation GetReclamation(Client client, CopyDetails copyDetails, DateTime eventDateTime, Invoice invoice)
        {
            return _dataRepository.GetEvent(
                _dataRepository.FindEvent(new Reclamation(client, copyDetails, eventDateTime,invoice))) as Reclamation;
        }

        public CopyDetails GetCopyDetails(Book book, decimal price, decimal tax, int count, string description)
        {
            return _dataRepository.GetCopyDetails(
                _dataRepository.FindCopyDetails(new CopyDetails(book, price, tax, count, description)));
        }


        public IEnumerable<Book> GetBooks()
        {
            return _dataRepository.GetAllBooks();
        }

        public IEnumerable<Client> GetClients()
        {
            return _dataRepository.GetAllClients();
        }

        public IEnumerable<CopyDetails> GetCopyDetailses()
        {
            return _dataRepository.GetAllCopyDetailses();
        }

        public IEnumerable<Event> GetEvents()
        {
            return _dataRepository.GetAllEvents();
        }


        public void UpdateClient(Client client)
        {
            _dataRepository.UpdateClient(client, _dataRepository.FindClient(client));
        }

        public void UpdateEvent(Invoice invoice)
        {
            _dataRepository.UpdateEvent(invoice, _dataRepository.FindEvent(invoice));
        }

        public void UpdateCopyDetails(CopyDetails copyDetails)
        {
            _dataRepository.UpdateCopyDetails(copyDetails, _dataRepository.FindCopyDetails(copyDetails));
        }

        public void UpdateBook(Book book)
        {
            _dataRepository.UpdateBook(book, _dataRepository.FindBook(book));
        }

        public void DeleteBook(Book book)
        {
            _dataRepository.DeleteBook(book);
        }

        public void DeleteClient(Client client)
        {
            _dataRepository.DeleteClient(client);
        }

        public void DeleteEvent(Invoice invoice)
        {
            _dataRepository.DeleteEvent(invoice);
        }

        public void DeleteCopyDetails(CopyDetails copyDetails)
        {
            _dataRepository.DeleteCopyDetails(copyDetails);
        }
    }
}