using System.Collections.Generic;
using BookStore.Model.Entities;

namespace BookStore.Model
{
    public interface IDataRepository
    {
        public void AddBook(Book book);
        public void AddClient(Client client);
        public void AddEvent(Event eEvent);
        public void AddCopyDetails(CopyDetails copyDetails);


        public Book GetBook(int key);
        public Client GetClient(int index);
        public Event GetEvent(int index);
        public CopyDetails GetCopyDetails(int index);


        public int FindBook(Book book);
        public int FindClient(Client client);
        public int FindCopyDetails(CopyDetails copyDetails);
        public int FindEvent(Event eEvent);


        public void UpdateBook(Book book, int index);
        public void UpdateClient(Client client, int index);
        public void UpdateEvent(Event eEvent, int index);
        public void UpdateCopyDetails(CopyDetails copyDetails, int index);


        public void DeleteBook(Book book);
        public void DeleteClient(Client client);
        public void DeleteEvent(Event eEvent);
        public void DeleteCopyDetails(CopyDetails copyDetails);

        public IEnumerable<Book> GetAllBooks();
        public IEnumerable<Client> GetAllClients();
        public IEnumerable<Event> GetAllEvents();
        public IEnumerable<CopyDetails> GetAllCopyDetails();
    }
}