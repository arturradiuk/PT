using System.Collections.Generic;
using System.Linq;
using BookStore.Model;
using BookStore.Model.Entities;

namespace BookStoreTest.Implementation
{
    public class DataRepositoryForTest : IDataRepository
    {
        private DataContext _dataContext = new DataContext();

        private IDataFiller _dataFiller;

        public DataRepositoryForTest(IDataFiller dataFiller)
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

        public int
            FindBook(Book book)
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


            foreach (KeyValuePair<int, Book> b in _dataContext.Books)
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

        #region event region

        public IEnumerable<Event> GetAllEvents()
        {
            return _dataContext.Events;
        }

        public void AddEvent(Event eEvent)
        {
            _dataContext.Events.Add(eEvent);
        }

        public int
            FindEvent(Event eEvent)
        {
            return _dataContext.Events.IndexOf(eEvent);
        }

        public void UpdateEvent(Event eEvent, int index)
        {
            _dataContext.Events[index] = eEvent;
        }

        public void DeleteEvent(Event eEvent)
        {
            _dataContext.Events.Remove(eEvent);
        }

        public Event GetEvent(int index)
        {
            return _dataContext.Events[index];
        }

        #endregion

        #region copydetails region

        public IEnumerable<CopyDetails> GetAllCopyDetails()
        {
            return _dataContext.AllCopyDetails;
        }

        public void AddCopyDetails(CopyDetails copyDetails)
        {
            _dataContext.AllCopyDetails.Add(copyDetails);
        }

        public int
            FindCopyDetails(
                CopyDetails copyDetails)
        {
            return _dataContext.AllCopyDetails.IndexOf(copyDetails);
        }

        public void UpdateCopyDetails(CopyDetails copyDetails, int index)
        {
            _dataContext.AllCopyDetails[index] = copyDetails;
        }

        public void DeleteCopyDetails(CopyDetails copyDetails)
        {
            _dataContext.AllCopyDetails.Remove(copyDetails);
        }

        public CopyDetails GetCopyDetails(int index)
        {
            return _dataContext.AllCopyDetails[index];
        }

        #endregion
    }
}