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
            Guid id = Guid.NewGuid();
            Book b1 = new Book(id, "Year 1984", "George Orwell", 1949, Genre.Fiction);

            Assert.Equal(id, b1.Id);
            Assert.Equal("Year 1984", b1.BookName);
            Assert.Equal("George Orwell", b1.AuthorName);
            Assert.Equal(1949, b1.Year);
            Assert.Equal(Genre.Fiction, b1.Genre);
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
            Guid id = Guid.NewGuid();
            Book b1 = new Book(id, "Year 1984", "George Orwell", 1949, Genre.Fiction);

            CopyDetails cd1 = new CopyDetails(id, b1, new decimal(35.99), new decimal(2.65), 1);
            
            Assert.Equal(id, cd1.Id);
            Assert.Equal(b1, cd1.Book);
            Assert.Equal(new decimal(35.99), cd1.Price);
            Assert.Equal(new decimal(2.65), cd1.Tax);
            Assert.Equal(1, cd1.Count);
        }

        [Fact]
        public void InvoiceTest()
        {
            Guid id = Guid.NewGuid();
            DateTimeOffset purchaseTime = DateTimeOffset.Now;
            Book b1 = new Book(id, "Year 1984", "George Orwell", 1949, Genre.Fiction);
            Client c1 = new Client("john.smith@edu.p.lodz.pl", "John", "Smith", "896-457-891");
            CopyDetails cd1 = new CopyDetails(id, b1, new decimal(35.99), new decimal(2.65), 1);
            
            Invoice inv1 = new Invoice(c1, cd1, id, purchaseTime);
            
            Assert.Equal(id, inv1.Id);
            Assert.Equal(c1, inv1.Client);
            Assert.Equal(cd1, inv1.CopyDetails);
            Assert.Equal(purchaseTime, inv1.PurchaseTime);
        }
    }
}