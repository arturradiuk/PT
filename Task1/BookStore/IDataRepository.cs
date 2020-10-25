using System;
using System.Collections.Generic;

namespace BookStore
{
    public interface IDataRepository
    {
        public void AddBook(Book book);
        public void AddClient(Client client);
        public void AddInvoice(Invoice invoice);
        public void AddCopyDetails(CopyDetails copyDetails);  
        
        public Book GetBook(Guid id);
        public Client GetClient(string email);
        public Invoice GetInvoice(Guid id);
        public CopyDetails GetCopyDetails(Guid id);
        
        public void UpdateBook(Book book, Guid id);
        public void UpdateClient(Client client, string email);
        public void UpdateInvoice(Invoice invoice, Guid id);
        public void UpdateCopyDetails(CopyDetails copyDetails, Guid id);

        
        public void DeleteBook(Book book);
        public void DeleteClient(Client client);
        public void DeleteInvoice(Invoice invoice);
        public void DeleteCopyDetails(CopyDetails copyDetails);
        
        public IEnumerable<Book> GetAllBooks();
        public IEnumerable<Client> GetAllClients();
        public IEnumerable<Invoice> GetAllInvoices();
        public IEnumerable<CopyDetails> GetAllCopyDetailses();
        
        
        
    }
}