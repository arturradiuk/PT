using System.Collections.Generic;
using System.Collections.ObjectModel;
using BookStore.Model.Entities;

namespace BookStore.Model
{
    public class DataContext
    {
        public List<Client> Clients = new List<Client>();
        public Dictionary<int, Book> Books = new Dictionary<int, Book>();
        public ObservableCollection<Event> Events = new ObservableCollection<Event>();
        public ObservableCollection<CopyDetails> AllCopyDetails = new ObservableCollection<CopyDetails>();
    }
}