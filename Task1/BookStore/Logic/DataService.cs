using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Model;
using BookStore.Model.Entities;

namespace BookStore.Logic
{
    public class DataService : IDataService
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

        public Reclamation ReturnBook(Invoice invoice, bool isBookFaulty,
            string description)
        {
            if (!this._dataRepository.GetAllEvents().Contains(invoice))
            {
                throw new InvalidOperationException("We don't have such invoice.");
            }

            Reclamation reclamation = new Reclamation(DateTime.Now, invoice, description, isBookFaulty);
            if (!isBookFaulty)
            {
                invoice.CopyDetails.Count += 1;
            }

            _dataRepository.UpdateCopyDetails(invoice.CopyDetails,
                this._dataRepository.FindCopyDetails(invoice.CopyDetails));
            _dataRepository.AddEvent(reclamation);
            return reclamation;
        }

        public IEnumerable<Event> GetEventsForTheBook(Book book)
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

            foreach (Book b in books)
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

        public IEnumerable<Event> GetEventsBetween(DateTime start, DateTime end)
        {
            return GetEvents().Where(eEvent =>
                (eEvent.EventDateTime.CompareTo(start) == 1) && (eEvent.EventDateTime.CompareTo(end) == -1));
        }

        public IEnumerable<Client> GetClientsForTheBook(Book book)
        {
            IEnumerable<Event> events = GetEventsForTheBook(book);
            List<Client> clients = new List<Client>();
            foreach (Event eEvent in events)
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

        public IEnumerable<CopyDetails> GetAllCopyDetails()
        {
            return _dataRepository.GetAllCopyDetails();
        }

        public IEnumerable<Event> GetEvents()
        {
            return _dataRepository.GetAllEvents();
        }


        public void UpdateClient(Client client)
        {
            _dataRepository.UpdateClient(client, _dataRepository.FindClient(client));
        }

        public void UpdateEvent(Event eEvent)
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