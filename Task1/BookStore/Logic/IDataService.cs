using System;
using System.Collections.Generic;
using BookStore.Model.Entities;

namespace BookStore.Logic
{
    public interface IDataService
    {
        void
            UpdateBookStock(CopyDetails copyDetails, int count);

        Invoice BuyBook(Client client, CopyDetails copyDetails);
        IEnumerable<Invoice> GetInvoicesForTheBook(Book book);
        IEnumerable<ValueTuple<Book, int>> GetBoughtBooksAndAmount();
        IEnumerable<Invoice> GetInvoicesForTheClient(Client client);
        IEnumerable<Book> GetInvoicesForTheBooksAuthorName(string authorName);

        IEnumerable<Invoice> GetInvoicesBetween(DateTime start, DateTime end) // todo check 
            ;

        IEnumerable<Client> GetClientsForTheBook(Book book);
        void AddBook(Book book);
        void AddClient(Client client);
        void AddCopyDetails(CopyDetails copyDetails);
        Book GetBook(string bookName, string author, int year);
        Client GetClient(string email, string firstName, string secondName, string phoneNumber);
        Invoice GetInvoice(Client client, CopyDetails copyDetails, DateTime purchaseTime);
        CopyDetails GetCopyDetails(Book book, decimal price, decimal tax, int count, string description);
        IEnumerable<Book> GetBooks();
        IEnumerable<Client> GetClients();
        IEnumerable<CopyDetails> GetCopyDetailses();
        IEnumerable<Invoice> GetInvoices();
        void UpdateClient(Client client);
        void UpdateInvoice(Invoice invoice);
        void UpdateCopyDetails(CopyDetails copyDetails);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        void DeleteClient(Client client);
        void DeleteInvoice(Invoice invoice);
        void DeleteCopyDetails(CopyDetails copyDetails);
    }
}