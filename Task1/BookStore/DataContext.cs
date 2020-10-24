using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BookStore
{
    public class DataContext
    {
        public List<Client> Clients = new List<Client>();
        public Dictionary<Guid, Book> Books = new Dictionary<Guid, Book>();
        public ObservableCollection<Invoice> Invoices = new ObservableCollection<Invoice>();
        public ObservableCollection<CopyDetails> CopyDetailses = new ObservableCollection<CopyDetails>();
    }
}