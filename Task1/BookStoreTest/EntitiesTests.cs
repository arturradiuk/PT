using System;
using Xunit;
using BookStore;

namespace BookStoreTest
{
    public class SimpleEntitiesTests
    {
        [Fact]
        public void BookTest()
        {
            Book b1 = new Book("Year 1984", "George Orwell", 1949);

            Assert.Equal("Year 1984", b1.BookName);
            Assert.Equal("George Orwell", b1.AuthorName);
            Assert.Equal(1949, b1.Year);
        }

        [Fact]
        public void ClientTest()
        {
            Client c1 = new Client("john.smith@edu.p.lodz.pl", "John", "Smith", "896-457-891");

            Assert.Equal("john.smith@edu.p.lodz.pl", c1.Email);
            Assert.Equal("John", c1.FirstName);
            Assert.Equal("Smith", c1.SecondName);
            Assert.Equal("896-457-891", c1.PhoneNumber);
        }

        [Fact]
        public void CopyDetailsTest()
        {
            Book b1 = new Book("Year 1984", "George Orwell", 1949);

            CopyDetails cd1 = new CopyDetails(b1, new decimal(35.99), new decimal(2.65), 1,"short description");
            
            Assert.Equal(b1, cd1.Book);
            Assert.Equal(new decimal(35.99), cd1.Price);
            Assert.Equal(new decimal(2.65), cd1.Tax);
            Assert.Equal(1, cd1.Count);
        }

        [Fact]
        public void InvoiceTest()
        {
            DateTime purchaseTime = DateTime.Now;
            Book b1 = new Book("Year 1984", "George Orwell", 1949);
            Client c1 = new Client("john.smith@edu.p.lodz.pl", "John", "Smith", "896-457-891");
            CopyDetails cd1 = new CopyDetails(b1, new decimal(35.99), new decimal(2.65), 1,"short description");
            
            Invoice inv1 = new Invoice(c1, cd1,  purchaseTime);
            
            Assert.Equal(c1, inv1.Client);
            Assert.Equal(cd1, inv1.CopyDetails);
            Assert.Equal(purchaseTime, inv1.PurchaseTime);
        }
    }
}