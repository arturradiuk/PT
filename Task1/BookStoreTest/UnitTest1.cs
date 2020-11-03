using System;
using BookStore;
using Xunit;

namespace BookStoreTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Book book = new Book(new Guid(),"bookName","authorName",2001,Genre.Biography );
            Assert.Equal(book,book);
        }
    }
}
