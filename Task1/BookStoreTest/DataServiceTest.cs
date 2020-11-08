using System;
using System.Collections.Immutable;
using System.Linq;
using BookStore;
using Xunit;

namespace BookStoreTest
{
    public class DataServiceTest
    {
        #region books test

        [Fact]
        public void UpdateBookStockTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);
            CopyDetails copyDetails = dataRepository.GetCopyDetails(0);
            dataService.UpdateBookStock(copyDetails, 1);

            Assert.Equal(1, dataRepository.GetCopyDetails(0).Count);
        }

        [Fact]
        public void BuyBookTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

            Client client = dataRepository.GetClient(2);
            CopyDetails copyDetails = dataRepository.GetCopyDetails(3);
            int originalCopyDetailsCount = copyDetails.Count;
            int totalInvoices = dataRepository.GetAllInvoices().ToImmutableHashSet().Count;
            dataService.BuyBook(client, copyDetails);
            Assert.Equal(originalCopyDetailsCount - 1, dataRepository.GetCopyDetails(3).Count);
            Assert.Equal(totalInvoices + 1, dataRepository.GetAllInvoices().ToImmutableHashSet().Count);
        }

        [Fact]
        public void GetInvoicesForTheBookTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);
            
            Book book = dataRepository.GetBook(1);
            var invoices = dataService.GetInvoicesForTheBook(book);
            foreach (var invoice in invoices)
            {
                Assert.Equal(book, invoice.CopyDetails.Book);
            }
        }

        [Fact]
        public void GetBoughtBooksAndAmountTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);
            DataService dataService = new DataService(dataRepository);

            var boughtBooks = dataService.GetBoughtBooksAndAmount();

            int booksCount = dataService.GetBooks().ToImmutableHashSet().Count;

            Assert.Equal(booksCount, boughtBooks.ToImmutableHashSet().Count);

            int totalInvoices = 0;
            foreach (var book in boughtBooks)
            {
                totalInvoices += book.Item2;
            }

            Assert.Equal(dataService.GetInvoices().ToImmutableHashSet().Count, totalInvoices);
        }
        
        #endregion
    }
}