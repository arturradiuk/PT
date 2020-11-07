using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;

namespace BookStore
{
    public interface IDataRepository
    {
        public void AddBook(Book book);
        public void AddClient(Client client);
        public void AddInvoice(Invoice invoice);
        public void AddCopyDetails(CopyDetails copyDetails);


        public Book GetBook(int key);
        public Client GetClient(int index);
        public Invoice GetInvoice(int index);
        public CopyDetails GetCopyDetails(int index);


        public int FindBook(Book book);
        public int FindClient(Client client);
        public int FindCopyDetails(CopyDetails copyDetails);
        public int FindInvoice(Invoice invoice);


        public void UpdateBook(Book book, int index);
        public void UpdateClient(Client client, int index);
        public void UpdateInvoice(Invoice invoice, int index);
        public void UpdateCopyDetails(CopyDetails copyDetails, int index);


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