using System;
using System.Collections.Generic;
using System.Linq;
using BookStore.Model.Entities;

namespace BookStore.Model
{
    public class DataRepository : IDataRepository
    {
        private DataContext _dataContext = new DataContext();

        private IDataFiller _dataFiller;

        public DataRepository(IDataFiller dataFiller)
        {
            this._dataFiller = dataFiller;
            this._dataFiller.Fill(this._dataContext);
        }


        private int _bookKey = 6;

        public int BookKey
        {
            get => _bookKey;

            set => _bookKey = value;
        }


        #region client region

        public IEnumerable<Client> GetAllClients()
        {
            return _dataContext.Clients;
        }

        public void AddClient(Client client)
        {
            if (_dataContext.Clients.Any(c => c.Email.Equals(client.Email)))
            {
                throw new ArgumentException($"Client with email {client.Email} already exists.");
            }

            _dataContext.Clients.Add(client);
        }

        public int FindClient(Client client)
        {
            if (_dataContext.Clients.Contains(client))
            {
                return _dataContext.Clients.IndexOf(client);
            }

            throw new ArgumentException("This client does not exist.");
        }

        public void UpdateClient(Client client, int index)
        {
            if (_dataContext.Clients.Count() > 0 && index >= 0 && index < _dataContext.Clients.Count())
            {
                _dataContext.Clients[index] = client;
                return;
            }

            throw new ArgumentException("The index is invalid.");
        }

        public void DeleteClient(Client client)
        {
            foreach (var eEvent in _dataContext.Events)
            {
                Invoice invoice = eEvent as Invoice;
                if (invoice != null)
                {

                    if (invoice.Client.Equals(client))
                    {
                        throw new ArgumentException(
                            "You can't delete this client, because this client is used in event.");
                    }
                }
            }

            if (!_dataContext.Clients.Remove(client))
            {
                throw new ArgumentException("Client does not exist.");
            }
        }

        public Client GetClient(int index)
        {
            if (_dataContext.Clients.Count() > 0 && index >= 0 && index < _dataContext.Clients.Count())
            {
                return _dataContext.Clients[index];
            }

            throw new ArgumentException("The index is invalid.");
        }

        #endregion

        #region book region

        public IEnumerable<Book> GetAllBooks()
        {
            return _dataContext.Books.Values;
        }

        public void AddBook(Book book)
        {
            if (_dataContext.Books.ContainsValue(book))
            {
                throw new ArgumentException($"This book already exists.");
            }

            _dataContext.Books.Add(this._bookKey, book);
            this._bookKey++;
        }

        public int
            FindBook(Book book)
        {
            if (_dataContext.Books.ContainsValue(book))
            {
                return _dataContext.Books.FirstOrDefault(b => b.Value.Equals(book)).Key;
            }

            throw new ArgumentException("This book does not exist.");
        }

        public void UpdateBook(Book book, int key)
        {
            if (_dataContext.Books.ContainsKey(key))
            {
                _dataContext.Books[key] = book;
                return;
            }

            throw new ArgumentException("Book with this key doesn't exist.");
        }

        public void DeleteBook(Book book)
        {
            foreach (var eEvent in _dataContext.Events)
            {
                Invoice invoice = eEvent as Invoice;
                if (invoice != null)
                {

                    if (invoice.CopyDetails.Book.Equals(book))
                    {
                        throw new ArgumentException(
                            "You can't delete this book, because this book is used in event.");
                    }
                }
            }

            int key = -1;


            foreach (var b in _dataContext.Books)
            {
                if (b.Value.Equals(book))
                {
                    key = b.Key;
                }
            }

            if (!_dataContext.Books.Remove(key))
            {
                throw new ArgumentException("Book with doesn't exist.");
            }
        }

        public Book GetBook(int key)
        {
            if (_dataContext.Books.ContainsKey(key))
            {
                return _dataContext.Books[key];
            }
            else
            {
                throw new ArgumentException("The index is invalid.");
            }
        }

        #endregion

        #region event region

        public IEnumerable<Event> GetAllEvents()
        {
            return _dataContext.Events;
        }

        public void AddEvent(Event eEvent)
        {
            if (_dataContext.Events.Any(i => i.Equals(eEvent)))
            {
                throw new ArgumentException($"This event already exists.");
            }

            _dataContext.Events.Add(eEvent);
        }

        public int
            FindEvent(Event eEvent) // todo should we place here some changes due to creating invoice and reclamation? 
        {
            if (this._dataContext.Events.Contains(eEvent))
            {
                return _dataContext.Events.IndexOf(eEvent);
            }

            throw new ArgumentException("This event does not exist.");
        }

        public void UpdateEvent(Event eEvent, int index)
        {
            if (_dataContext.Events.Count() > 0 && index >= 0 && index < _dataContext.Events.Count())
            {
                _dataContext.Events[index] = eEvent;
            }

            else throw new ArgumentException("The index is invalid.");
        }

        public void DeleteEvent(Event eEvent) // todo explain and test
        {
            if (null != eEvent as Reclamation)
            {
                if (!_dataContext.Events.Remove(eEvent))
                {
                    throw new ArgumentException("Event does not exist.");
                }

                return;
            }

            Invoice invoice = eEvent as Invoice;

            foreach (var e in _dataContext.Events)
            {
                if (null != e as Reclamation)
                {
                    if ((e as Reclamation).Invoice.Equals(invoice))
                    {
                        throw new ArgumentException(
                            "You can't delete this invoice, because this invoice is used in reclamation.");
                    }
                }
            }

            _dataContext.Events.Remove(eEvent);
        }

        public Event GetEvent(int index)
        {
            if (_dataContext.Events.Count() > 0 && index >= 0 && index < _dataContext.Events.Count())
            {
                return _dataContext.Events[index];
            }

            throw new ArgumentException("The index is invalid.");
        }

        #endregion

        #region copydetails region

        public IEnumerable<CopyDetails> GetAllCopyDetailses()
        {
            return _dataContext.CopyDetailses;
        }

        public void AddCopyDetails(CopyDetails copyDetails)
        {
            if (_dataContext.CopyDetailses.Contains(copyDetails))
            {
                throw new ArgumentException($"This copyDetails already exists.");
            }

            _dataContext.CopyDetailses.Add(copyDetails);
        }

        public int
            FindCopyDetails(
                CopyDetails copyDetails)
        {
            if (this._dataContext.CopyDetailses.Contains(copyDetails))
            {
                return _dataContext.CopyDetailses.IndexOf(copyDetails);
            }

            throw new ArgumentException("This copy details does not exist.");
        }

        public void UpdateCopyDetails(CopyDetails copyDetails, int index)
        {
            if (_dataContext.CopyDetailses.Count() > 0 && index >= 0 && index < _dataContext.CopyDetailses.Count())
            {
                _dataContext.CopyDetailses[index] = copyDetails;
                return;
            }

            throw new ArgumentException("The index is invalid.");
        }

        public void DeleteCopyDetails(CopyDetails copyDetails)
        {
            foreach (var eEvent in _dataContext.Events)
            {

                Invoice invoice = eEvent as Invoice;
                if (invoice != null)
                {

                    if (invoice.CopyDetails.Equals(copyDetails))
                    {
                        throw new ArgumentException(
                            "You can't delete this copyDetails, because this client is used in copyDetails.");
                    }
                }
                
            }

            if (!_dataContext.CopyDetailses.Remove(copyDetails))
            {
                throw new ArgumentException("CopyDetails does not exist.");
            }
        }

        public CopyDetails GetCopyDetails(int index)
        {
            if (_dataContext.CopyDetailses.Count() > 0 && index >= 0 && index < _dataContext.CopyDetailses.Count())
            {
                return _dataContext.CopyDetailses[index];
            }

            throw new ArgumentException("The index is invalid.");
        }

        #endregion
    }
}