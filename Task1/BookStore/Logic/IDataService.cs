using System;
using System.Collections.Generic;
using BookStore.Model.Entities;

namespace BookStore.Logic
{
    public interface IDataService
    {
        void
            UpdateBookStock(CopyDetails copyDetails, int count);

        Invoice BuyBook(Client client, CopyDetails copyDetails, string description);
        IEnumerable<Event> GetEventsForTheBook(Book book);
        IEnumerable<ValueTuple<Book, int>> GetBoughtBooksAndAmount();
        IEnumerable<Event> GetEventsForTheClient(Client client);
        IEnumerable<Event> GetEventsBetween(DateTime start, DateTime end);

        IEnumerable<Client> GetClientsForTheBook(Book book);
        void AddBook(Book book);
        void AddClient(Client client);
        void AddCopyDetails(CopyDetails copyDetails);
        Book GetBook(string bookName, string author, int year);
        Client GetClient(string email, string firstName, string secondName, string phoneNumber);
        Invoice GetInvoice(Client client, CopyDetails copyDetails, DateTime purchaseTime, string description);
        CopyDetails GetCopyDetails(Book book, decimal price, decimal tax, int count, string description);
        IEnumerable<Book> GetBooks();
        IEnumerable<Client> GetClients();
        IEnumerable<CopyDetails> GetCopyDetailses();
        IEnumerable<Event> GetEvents();
        void UpdateClient(Client client);
        void UpdateEvent(Event eEvent);
        void UpdateCopyDetails(CopyDetails copyDetails);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        void DeleteClient(Client client);
        void DeleteEvent(Event eEvent);
        void DeleteCopyDetails(CopyDetails copyDetails);

        Reclamation ReturnBook(Invoice invoice, bool isBookFaulty,
            string description);
    }
}