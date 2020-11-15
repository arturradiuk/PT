using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BookStore.Model;
using BookStore.Model.Entities;



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


namespace BookStore.Logic
{
    public class DataService : IDataService
    {
        private IDataRepository _dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // public void UpdateBookStock(Book book, int count) 
        public void
            UpdateBookStock(CopyDetails copyDetails, int count) // todo is is good idea to place here copydetails
        {
            if (count < 0)
            {
                throw new InvalidOperationException("You can't update book stock with negative number.");
            }

            copyDetails.Count = count;
            _dataRepository.UpdateCopyDetails(copyDetails, _dataRepository.FindCopyDetails(copyDetails));
        }

        public Invoice BuyBook(Client client, CopyDetails copyDetails, string description)
        {
            if (copyDetails.Count <= 0)
            {
                throw new InvalidOperationException("We don't have these books, wait for book stock updating.");
            }

            Invoice createdInvoice = new Invoice(client, copyDetails, DateTime.Now, description);
            copyDetails.Count -= 1;
            _dataRepository.UpdateCopyDetails(copyDetails, this._dataRepository.FindCopyDetails(copyDetails));
            _dataRepository.AddEvent(createdInvoice);
            return createdInvoice;
        }

        public Reclamation ReturnBook(Client client, CopyDetails copyDetails, Invoice invoice, bool isBookFaulty,
            string description)
        {
            if (!this._dataRepository.GetAllEvents().Contains(invoice))
            {
                throw new InvalidOperationException("We don't have such invoice.");
            }

            Reclamation reclamation = new Reclamation(DateTime.Now, invoice, description, isBookFaulty);
            if (!isBookFaulty)
            {
                copyDetails.Count += 1;
            }

            _dataRepository.UpdateCopyDetails(copyDetails, this._dataRepository.FindCopyDetails(copyDetails));
            _dataRepository.AddEvent(reclamation);
            return reclamation;
        }

        public IEnumerable<Event> GetInvoicesForTheBook(Book book)
        {
            return GetEvents().Where(eEvent =>
                {
                    Reclamation r = eEvent as Reclamation;
                    Invoice i = eEvent as Invoice;
                    if (r != null)
                    {
                        if (r.Invoice.CopyDetails.Book.Equals(book))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (i.CopyDetails.Book.Equals(book))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            );
        }

        public IEnumerable<ValueTuple<Book, int>> GetBoughtBooksAndAmount()
        {
            List<ValueTuple<Book, int>> temp = new List<(Book, int)>();
            IEnumerable<Book> books = GetBooks();

            foreach (var b in books)
            {
                int amount = GetInvoicesForTheBook(b).Count();
                if (amount != 0)
                {
                    temp.Add((b, amount));
                }
            }

            return temp;
        }

        public IEnumerable<Event> GetInvoicesForTheClient(Client client)
        {
            return GetEvents().Where(eEvent =>
                {
                    Reclamation r = eEvent as Reclamation;
                    Invoice i = eEvent as Invoice;
                    if (r != null)
                    {
                        if (r.Invoice.Client.Email.Equals(client.Email))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (i.Client.Email.Equals(client.Email))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            );
        }


        // public IEnumerable<Book> GetEventForTheBooksAuthorName(string authorName)
        // {
            // return GetBooks().Where(book => book.AuthorName.Equals(authorName));
        // }

        public IEnumerable<Event> GetEventsBetween(DateTime start, DateTime end) // todo check 
        {
            return GetEvents().Where(eEvent =>
                (eEvent.EventDateTime.CompareTo(start) == 1) && (eEvent.EventDateTime.CompareTo(end) == -1));
        }

        public IEnumerable<Client> GetClientsForTheBook(Book book)
        {
            IEnumerable<Event> events = GetInvoicesForTheBook(book);
            List<Client> clients = new List<Client>();
            foreach (var eEvent in events)
            {
                Invoice invoice = eEvent as Invoice;
                if (null != invoice)
                {
                    clients.Add(invoice.Client);
                }
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


        // tood check for the index using in this layer, its necessity 
        public Book GetBook(string bookName, string author, int year)
        {
            return _dataRepository.GetBook(_dataRepository.FindBook(new Book(bookName, author, year)));
        }

        public Client GetClient(string email, string firstName, string secondName, string phoneNumber)
        {
            return _dataRepository.GetClient(
                _dataRepository.FindClient(new Client(email, firstName, secondName, phoneNumber)));
        }

        public Invoice GetInvoice(Client client, CopyDetails copyDetails, DateTime eventDateTime, string description)
        {
            return _dataRepository.GetEvent(
                _dataRepository.FindEvent(new Invoice(client, copyDetails, eventDateTime, description))) as Invoice;
        }

        public Reclamation GetReclamation(DateTime eventDateTime,
            Invoice invoice, string description)
        {
            return _dataRepository.GetEvent(
                _dataRepository.FindEvent(new Reclamation(eventDateTime, invoice, description))) as Reclamation;
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

        public void UpdateEvent(Event eEvent) // todo change here 
        {
            _dataRepository.UpdateEvent(eEvent, _dataRepository.FindEvent(eEvent));
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

        public void DeleteEvent(Event eEvent)
        {
            _dataRepository.DeleteEvent(eEvent);
        }

        public void DeleteCopyDetails(CopyDetails copyDetails)
        {
            _dataRepository.DeleteCopyDetails(copyDetails);
        }

        
        
    }

}