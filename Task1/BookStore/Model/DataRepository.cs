using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore
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
            foreach (var invoice in _dataContext.Invoices)
            {
                if (invoice.Client.Equals(client))
                {
                    throw new ArgumentException(
                        "You can't delete this client, because this client is used in invoice.");
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
            foreach (var invoice in _dataContext.Invoices)
            {
                if (invoice.CopyDetails.Book.Equals(book))
                {
                    throw new ArgumentException(
                        "You can't delete this book, because this book is used in invoice."); 
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

        #region invoice region 

        public IEnumerable<Invoice> GetAllInvoices()
        {
            return _dataContext.Invoices;
        }

        public void AddInvoice(Invoice invoice)
        {
            if (_dataContext.Invoices.Any(i => i.Equals(invoice)))
            {
                throw new ArgumentException($"This invoice already exists.");
            }

            _dataContext.Invoices.Add(invoice);
        }

        public int FindInvoice(Invoice invoice)
        {
            if (this._dataContext.Invoices.Contains(invoice))
            {
                return _dataContext.Invoices.IndexOf(invoice);
            }

            throw new ArgumentException("This invoice does not exist.");
        }

        public void UpdateInvoice(Invoice invoice, int index)
        {
            if (_dataContext.Invoices.Count() > 0 && index >= 0 && index < _dataContext.Invoices.Count())
            {
                _dataContext.Invoices[index] = invoice;
            }

            else throw new ArgumentException("The index is invalid.");
        }

        public void DeleteInvoice(Invoice invoice)
        {
            if (!_dataContext.Invoices.Remove(invoice))
            {
                throw new ArgumentException("Invoice does not exist.");
            }
        }

        public Invoice GetInvoice(int index) 
        {
            if (_dataContext.Invoices.Count() > 0 && index >= 0 && index < _dataContext.Invoices.Count())
            {
                return _dataContext.Invoices[index];
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
            foreach (var invoice in _dataContext.Invoices)
            {
                if (invoice.CopyDetails.Equals(copyDetails))
                {
                    throw new ArgumentException(
                        "You can't delete this copyDetails, because this client is used in copyDetails."); 
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