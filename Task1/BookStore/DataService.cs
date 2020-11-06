using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore
{
    /*public class DataService
    {
        private IDataRepository _dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }


        public Book FindBook(string bookName, string authorName)
        {
        }
        
        
        
        public void UpdateBookStock(Book book, int count)
        {
            if (count < 0)
            {
                throw new InvalidOperationException("You can't update book stock with negative number.");
            }

            CopyDetails copyDetails = this.GetCopyDetailses().FirstOrDefault(cd => cd.Book.Id.Equals(book.Id));
            copyDetails.Count = count;
            _dataRepository.UpdateCopyDetails(copyDetails, copyDetails.Id);
        }

        public IEnumerable<ValueTuple<Guid,int>> GetBoughtBooksAndAmount()
        {
            List<ValueTuple<Guid,int>> temp = new List<(Guid, int)>();
            IEnumerable<Book> books = GetBooks();
            foreach (var b in books)
            {
                Guid id = b.Id;
                int amount = GetInvoicesForTheBook(b).Count();
                if (amount != 0)
                {
                    temp.Add((id,amount));
                }
            }

            return temp;
        }
        public Invoice BuyBook(Client client, CopyDetails copyDetails)
        {
            if (copyDetails.Count <= 0)
            {
                throw new InvalidOperationException("We don't have these books, wait for book stock updating.");
            }

            Invoice createdInvoice = new Invoice(client, copyDetails, new Guid(), DateTimeOffset.Now);
            copyDetails.Count -= 1;
            _dataRepository.UpdateCopyDetails(copyDetails, copyDetails.Id);
            _dataRepository.AddInvoice(createdInvoice);
            return createdInvoice;
        }

        public IEnumerable<Invoice> GetInvoicesForTheBook(Book book)
        {
            return GetInvoices().Where(invoice => invoice.CopyDetails.Book.Id.Equals(book.Id));
        }

        public IEnumerable<Invoice> GetInvoicesForTheClient(Client client)
        {
            return GetInvoices().Where(invoice => invoice.Client.Email.Equals(client.Email));
        }

        public IEnumerable<Book> GetBooksWithGenre(Genre genre)
        {
            return _dataRepository.GetAllBooks().Where(book => book.Genre.Equals(genre));
        }

        public IEnumerable<Book> GetInvoicesForTheBooksAuthorName(string authorName)
        {
            return GetBooks().Where(book => book.AuthorName.Equals(authorName));
        }

        public IEnumerable<Invoice> GetInvoicesBetween(DateTimeOffset start, DateTimeOffset end)
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


        public Book GetBook(Guid id)
        {
            return _dataRepository.GetBook(id);
        }

        public Client GetClient(string email)
        {
            return _dataRepository.GetClient(email);
        }

        public Invoice GetInvoice(Guid id)
        {
            return _dataRepository.GetInvoice(id);
        }

        public CopyDetails GetCopyDetails(Guid id)
        {
            return _dataRepository.GetCopyDetails(id);
        }

        public void UpdateBook(Book book, Guid id)
        {
            _dataRepository.UpdateBook(book, id);
        }

        public void UpdateClient(Client client, string email)
        {
            _dataRepository.UpdateClient(client, email);
        }

        public void UpdateInvoice(Invoice invoice, Guid id)
        {
            _dataRepository.UpdateInvoice(invoice, id);
        }

        public void UpdateCopyDetails(CopyDetails copyDetails, Guid id)
        {
            _dataRepository.UpdateCopyDetails(copyDetails, id);
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
    }*/
}