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

        public void AddBook(Book book)
        {
            if (_dataContext.Books.ContainsKey(book.Id))
            {
                throw new ArgumentException($"Book with Id {book.Id} already exists.");
            }

            _dataContext.Books.Add(book.Id, book);
        }

        public void AddClient(Client client)
        {
            if (_dataContext.Clients.Any(c => c.Email.Equals(client.Email)))
            {
                throw new ArgumentException($"Client with email {client.Email} already exists.");
            }

            _dataContext.Clients.Add(client);
        }

        public void AddInvoice(Invoice invoice)
        {
            if (_dataContext.Invoices.Any(i => i.Id.Equals(invoice.Id)))
            {
                throw new ArgumentException($"Invoice with Id {invoice.Id} already exists.");
            }

            _dataContext.Invoices.Add(invoice);
        }

        public void AddCopyDetails(CopyDetails copyDetails)
        {
            if (_dataContext.CopyDetailses.Any(c => c.Id.Equals(copyDetails.Id)))
            {
                throw new ArgumentException($"CopyDetails with Id {copyDetails.Id} already exists.");
            }

            _dataContext.CopyDetailses.Add(copyDetails);
        }


        public Book GetBook(Guid id)
        {
            return _dataContext.Books[id];
        }

        public Client GetClient(string email)
        {
            return _dataContext.Clients.FirstOrDefault(c => c.Email.Equals(email));
        }

        public Invoice GetInvoice(Guid id)
        {
            return _dataContext.Invoices.FirstOrDefault(i => i.Id.Equals(id));
        }

        public CopyDetails GetCopyDetails(Guid id)
        {
            return _dataContext.CopyDetailses.FirstOrDefault(c => c.Id.Equals(id));
        }


        public void UpdateBook(Book book, Guid id)
        {
            if (book.Id.Equals(id))
            {
                throw new ArgumentException($"Updating of the book Id is prohibited.");
            }

            if (_dataContext.Books.ContainsKey(id))
            {
                _dataContext.Books[id] = book;
            }

            throw new ArgumentException("Book with this Id doesn't exist.");
        }

        public void UpdateClient(Client client, string email)
        {
            if (client.Email.Equals(email))
            {
                throw new ArgumentException($"Updating of the client email is prohibited.");
            }

            int index = this._dataContext.Clients.FindIndex(c => c.Email.Equals(email));
            if (index == -1)
            {
                throw new ArgumentException("Client with this email doesn't exist.");
            }

            _dataContext.Clients[index] = client;
        }

        public void UpdateInvoice(Invoice invoice, Guid id)
        {
            if (invoice.Id.Equals(id))
            {
                throw new ArgumentException($"Updating of the invoice Id is prohibited.");
            }

            int index = _dataContext.Invoices.IndexOf(invoice);

            for (int i = 0; i < _dataContext.Invoices.Count; i++)
            {
                if (!(_dataContext.Invoices[i].Id.Equals(id))) continue;
                _dataContext.Invoices[i] = invoice;
                return;
            }

            throw new ArgumentException("Invoice with this Id doesn't exist.");
        }

        public void UpdateCopyDetails(CopyDetails copyDetails, Guid id)
        {
            if (copyDetails.Id.Equals(id))
            {
                throw new ArgumentException($"Updating of the copyDetails Id is prohibited.");
            }

            for (int i = 0; i < _dataContext.CopyDetailses.Count; i++)
            {
                if (!(_dataContext.CopyDetailses[i].Id.Equals(id))) continue;
                _dataContext.CopyDetailses[i] = copyDetails;
                return;
            }

            throw new ArgumentException("CopyDetails with this Id doesn't exist.");
        }


        public void DeleteBook(Book book)
        {
            if (_dataContext.Books.ContainsKey(book.Id))
            {
                _dataContext.Books.Remove(book.Id);
            }

            throw new ArgumentException("Book with this Id doesn't exist.");
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