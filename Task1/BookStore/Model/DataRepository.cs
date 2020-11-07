using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore
{
    public class DataRepository : IDataRepository
    {
        private DataContext _dataContext = new DataContext();

        private IDataFiller _dataFiller;

        // in this way we avoid the creating of id var and provide the data cohesion
        // private int _bookKey = 0;
        private int _bookKey = 6; // there is setter to provide the start number
        public int BookKey
        {
            get => _bookKey;
            set => _bookKey = value; // check if the number greater than 8 
        }

        // todo divide into regions 

        public int FindBook(Book book) // todo need to be checked what is returning in case of not founding the object, the planing value is -1
        {
            return _dataContext.Books.FirstOrDefault(b => b.Equals(book)).Key; 
        }

        public int FindClient(Client client)// todo need to be checked what is returning in case of not founding the object, the planing value is -1
        {
            return _dataContext.Clients.IndexOf(client);
        }

        public int FindCopyDetails(CopyDetails copyDetails)// todo need to be checked what is returning in case of not founding the object, the planing value is -1
        {
            return _dataContext.CopyDetailses.IndexOf(copyDetails);
        }

        public int FindInvoice(Invoice invoice)// todo need to be checked what is returning in case of not founding the object, the planing value is -1
        {
            return _dataContext.Invoices.IndexOf(invoice);
        }


        public DataRepository(IDataFiller dataFiller)
        {
            this._dataFiller = dataFiller;
            this._dataFiller.Fill(this._dataContext);
        }


        public void AddBook(Book book) // todo check for the duplicate
        {
            // if()
            _dataContext.Books.Add(this._bookKey, book);
            this._bookKey++;
        }

        public void AddClient(Client client) // done
        {
            if (_dataContext.Clients.Any(c => c.Email.Equals(client.Email)))
            {
                throw new ArgumentException($"Client with email {client.Email} already exists.");
            }

            _dataContext.Clients.Add(client);
        }

        // todo where should we check the data duplicates
        public void AddInvoice(Invoice invoice) // changed 
        {
            if (_dataContext.Invoices.Any(i => i.Equals(invoice)))
            {
                throw new ArgumentException($"This invoice already exists.");
            }

            _dataContext.Invoices.Add(invoice);
        }

        public void AddCopyDetails(CopyDetails copyDetails) // changed
        {
            if (_dataContext.CopyDetailses.Any(c => c.Equals(copyDetails)))
            {
                throw new ArgumentException($"This copyDetails already exists.");
            }

            _dataContext.CopyDetailses.Add(copyDetails);
        }


        public Book GetBook(int key) // changed
        {
            return _dataContext.Books[key];
        }

        public Client GetClient(int index) // changed
        {
            return _dataContext.Clients[index];
        }

        public Invoice GetInvoice(int index) // changed
        {
            return _dataContext.Invoices[index];
        }

        public CopyDetails GetCopyDetails(int index) //changed
        {
            return _dataContext.CopyDetailses[index];
        }


        public void UpdateBook(Book book, int key) // changed 
        {
            if (_dataContext.Books.ContainsKey(key))
            {
                _dataContext.Books[key] = book;
            }

            throw new ArgumentException("Book with this key doesn't exist.");
        }

        public void UpdateClient(Client client, int index) // changed
        {
            _dataContext.Clients[index] = client;
        }

        public void UpdateInvoice(Invoice invoice, int index) // changed // todo check for throw necessity 
        {
            _dataContext.Invoices[index] = invoice;
        }

        public void UpdateCopyDetails(CopyDetails copyDetails, int index)
        {
            _dataContext.CopyDetailses[index] = copyDetails;
        }


        public void DeleteBook(Book book)
        {
            _dataContext.Books.Remove(_dataContext.Books.FirstOrDefault(b => b.Value == book).Key);
        }

        public void DeleteClient(Client client)
        {
            if (!_dataContext.Clients.Remove(client))
            {
                throw new ArgumentException("Client with this Id doesn't exist.");
            }
        }

        public void DeleteInvoice(Invoice invoice)
        {
            if (!_dataContext.Invoices.Remove(invoice))
            {
                throw new ArgumentException("Invoice with this Id doesn't exist.");
            }
        }

        public void DeleteCopyDetails(CopyDetails copyDetails)
        {
            if (!_dataContext.CopyDetailses.Remove(copyDetails))
            {
                throw new ArgumentException("CopyDetails with this Id doesn't exist.");
            }
        }


        public IEnumerable<Book> GetAllBooks()
        {
            return _dataContext.Books.Values;
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _dataContext.Clients;
        }

        public IEnumerable<Invoice> GetAllInvoices()
        {
            return _dataContext.Invoices;
        }

        public IEnumerable<CopyDetails> GetAllCopyDetailses()
        {
            return _dataContext.CopyDetailses;
        }
        
        
    }
}