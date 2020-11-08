using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookStore
{
    public class DataService //:IDataService ?
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
            _dataRepository.AddInvoice(createdInvoice);
            return createdInvoice;
        }

        public IEnumerable<Invoice> GetInvoicesForTheBook(Book book)
        {
            return GetInvoices().Where(invoice => invoice.CopyDetails.Book.Equals(book));
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

        public IEnumerable<Invoice> GetInvoicesForTheClient(Client client)
        {
            return GetInvoices().Where(invoice => invoice.Client.Email.Equals(client.Email));
        }


        public IEnumerable<Book> GetInvoicesForTheBooksAuthorName(string authorName)
        {
            return GetBooks().Where(book => book.AuthorName.Equals(authorName));
        }

        public IEnumerable<Invoice> GetInvoicesBetween(DateTime start, DateTime end) // todo check 
        {
            return GetInvoices().Where(invoice =>
                (invoice.PurchaseTime.CompareTo(start) == 1) && (invoice.PurchaseTime.CompareTo(end) == -1));
        }

        public IEnumerable<Client> GetClientsForTheBook(Book book)
        {
            IEnumerable<Invoice> invoices = GetInvoicesForTheBook(book);
            List<Client> clients = new List<Client>();
            foreach (var i in invoices)
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

        public Invoice GetInvoice(Client client, CopyDetails copyDetails, DateTime purchaseTime)
        {
            return _dataRepository.GetInvoice(
                _dataRepository.FindInvoice(new Invoice(client, copyDetails, purchaseTime)));
        }

        public CopyDetails GetCopyDetails(Book book, decimal price, decimal tax, int count, string description)
        {
            return _dataRepository.GetCopyDetails(
                _dataRepository.FindCopyDetails(new CopyDetails(book, price, tax, count, description)));
        }

        //
        /*public Book GetBook(int index)
        {
            return _dataRepository.GetBook(index);
        }

        public Client GetClient(int index)
        {
            return _dataRepository.GetClient(index);
        }

        public Invoice GetInvoice(int index)
        {
            return _dataRepository.GetInvoice(index);
        }

        public CopyDetails GetCopyDetails(int index)
        {
            return _dataRepository.GetCopyDetails(index);
        }*/
        //


        #region GetSets Region

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

        public IEnumerable<Invoice> GetInvoices()
        {
            return _dataRepository.GetAllInvoices();
        }

        #endregion


        public void UpdateClient(Client client)
        {
            _dataRepository.UpdateClient(client, _dataRepository.FindClient(client));
        }

        public void UpdateInvoice(Invoice invoice)
        {
            _dataRepository.UpdateInvoice(invoice, _dataRepository.FindInvoice(invoice));
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

        public void DeleteInvoice(Invoice invoice)
        {
            _dataRepository.DeleteInvoice(invoice);
        }

        public void DeleteCopyDetails(CopyDetails copyDetails)
        {
            _dataRepository.DeleteCopyDetails(copyDetails);
        }
    }
}