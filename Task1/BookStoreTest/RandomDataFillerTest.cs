using System.Linq;
using BookStore;
using Xunit;

namespace BookStoreTest
{
    public class RandomDataFillerTest
    {
        [Fact]
        public void RandomDataFillerLengthDataTest()
        {
            RandomDataFiller randomDataFiller = new RandomDataFiller(8,20,6);
            DataRepository dataRepository = new DataRepository(randomDataFiller);

            Assert.Equal(8,dataRepository.GetAllBooks().Count());
            Assert.Equal(8,dataRepository.GetAllCopyDetailses().Count());
            Assert.Equal(20,dataRepository.GetAllInvoices().Count());
            Assert.Equal(6,dataRepository.GetAllClients().Count());

        }
    }
}