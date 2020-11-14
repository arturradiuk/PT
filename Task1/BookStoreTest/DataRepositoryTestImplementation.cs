using BookStore;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreTest
{
    public class DataRepositoryTestImplementation : IDataRepository
    {
        private DataContext _dataContext = new DataContext();

        private IDataFiller _dataFiller;

        public DataRepositoryTestImplementation(IDataFiller dataFiller)
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
            _dataContext.Clients.Add(client);
        }

        public int FindClient(Client client)
        {
            return _dataContext.Clients.IndexOf(client);
        }

        public void UpdateClient(Client client, int index)
        {
            _dataContext.Clients[index] = client;
        }

        public void DeleteClient(Client client)
        {
            _dataContext.Clients.Remove(client);
        }

        public Client GetClient(int index)
        {
            return _dataContext.Clients[index];
        }

        #endregion

        #region book region

        public IEnumerable<Book> GetAllBooks()
        {
            return _dataContext.Books.Values;
        }

        public void AddBook(Book book)
        {
            _dataContext.Books.Add(this._bookKey, book);
            this._bookKey++;
        }

        public int FindBook(Book book)
        {
            return _dataContext.Books.FirstOrDefault(b => b.Value.Equals(book)).Key;
        }

        public void UpdateBook(Book book, int key)
        {
            _dataContext.Books[key] = book;
        }

        public void DeleteBook(Book book)
        {
            int key = -1;


            foreach (var b in _dataContext.Books)
            {
                if (b.Value.Equals(book))
                {
                    key = b.Key;
                }
            }

            _dataContext.Books.Remove(key);
        }

        public Book GetBook(int key)
        {
            return _dataContext.Books[key];
        }

        #endregion

        #region invoice region

        public IEnumerable<Invoice> GetAllInvoices()
        {
            return _dataContext.Invoices;
        }

        public void AddInvoice(Invoice invoice)
        {
            _dataContext.Invoices.Add(invoice);
        }

        public int FindInvoice(Invoice invoice)
        {
            return _dataContext.Invoices.IndexOf(invoice);
        }

        public void UpdateInvoice(Invoice invoice, int index)
        {
            _dataContext.Invoices[index] = invoice;
        }

        public void DeleteInvoice(Invoice invoice)
        {
            _dataContext.Invoices.Remove(invoice);
        }

        public Invoice GetInvoice(int index)
        {
            return _dataContext.Invoices[index];
        }

        #endregion

        #region copydetails region

        public IEnumerable<CopyDetails> GetAllCopyDetailses()
        {
            return _dataContext.CopyDetailses;
        }

        public void AddCopyDetails(CopyDetails copyDetails)
        {
            _dataContext.CopyDetailses.Add(copyDetails);
        }

        public int FindCopyDetails(CopyDetails copyDetails)
        {
            return _dataContext.CopyDetailses.IndexOf(copyDetails);
        }

        public void UpdateCopyDetails(CopyDetails copyDetails, int index)
        {
            _dataContext.CopyDetailses[index] = copyDetails;
        }

        public void DeleteCopyDetails(CopyDetails copyDetails)
        {
            _dataContext.CopyDetailses.Remove(copyDetails);
        }

        public CopyDetails GetCopyDetails(int index)
        {
            return _dataContext.CopyDetailses[index];
        }

        #endregion
    }
}