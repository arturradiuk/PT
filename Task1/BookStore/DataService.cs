using System.Collections.Generic;

namespace BookStore
{
    public class DataService
    {
        private DataRepository _dataRepository;

        public DataService(DataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public IEnumerable<Book> getBooks()
        {
            return _dataRepository.GetAllBooks();
        }
        public IEnumerable<Client> getClients()
        {
            return _dataRepository.GetAllClients();
        }
        public IEnumerable<CopyDetails> getCopyDetailses()
        {
            return _dataRepository.GetAllCopyDetailses();
        }
        public IEnumerable<Invoice> getInvoices()
        {
            return _dataRepository.GetAllInvoices();
        }

        // public IEnumerable<Invoice> getInvoicesForTheClient(Client client)
        // public IEnumerable<Book> getInvoicesForTheBook(Book book)
        


    }
}