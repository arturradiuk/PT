using System;
using System.Linq;
using BookStore;
using Xunit;

namespace BookStoreTest
{
    public class DataRepositoryTest
    {
        #region client test region

        [Fact]
        public void GetAllClientsTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            Assert.Equal(5, dataRepository.GetAllClients().Count());
        }

        [Fact]
        public void AddClientValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            dataRepository.AddClient(client);

            Assert.Equal(6, dataRepository.GetAllClients().Count());
            Assert.Equal(client, dataRepository.GetClient(5));
        }

        [Fact]
        public void AddClientInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            dataRepository.AddClient(client);

            Assert.Throws<ArgumentException>(() => dataRepository.AddClient(client));
        }

        [Fact]
        public void FindClientValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            dataRepository.AddClient(client);
            Assert.StrictEqual(5, dataRepository.FindClient(client));
        }

        [Fact]
        public void FindClientInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            Assert.Throws<ArgumentException>(() => dataRepository.FindClient(client));
        }

        [Fact]
        public void UpdateClientValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);
            dataRepository.AddClient(client);

            Assert.Equal("Lolek", dataRepository.GetClient(5).FirstName);
            client.FirstName = "Tola";
            dataRepository.UpdateClient(client, 5);
            Assert.Equal("Tola", dataRepository.GetClient(5).FirstName);
        }

        [Fact]
        public void UpdateClientInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);
            dataRepository.AddClient(client);

            Assert.Equal("Lolek", dataRepository.GetClient(5).FirstName);
            client.FirstName = "Tola";

            Assert.Throws<ArgumentException>(() => dataRepository.UpdateClient(client, 105));
        }

        [Fact]
        public void DeleteClientValidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);
            dataRepository.AddClient(client);

            Assert.Equal(6, dataRepository.GetAllClients().Count());
            dataRepository.DeleteClient(client);
            Assert.Equal(5, dataRepository.GetAllClients().Count());
        }

        [Fact]
        public void DeleteClientInvalidValueTest()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            Assert.Equal(5, dataRepository.GetAllClients().Count());
            Assert.Throws<ArgumentException>(() => dataRepository.DeleteClient(client));
        }
        
        [Fact]
        public void GetClientValidValue()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);
            dataRepository.AddClient(client);
            Assert.NotNull(dataRepository.GetClient(dataRepository.FindClient(client)));
        }

        [Fact]
        public void GetClientInvalidValue()
        {
            ConstantDataFiller constantDataFiller = new ConstantDataFiller();
            DataRepository dataRepository = new DataRepository(constantDataFiller);

            const string email = "student.gmail.com";
            const string firstName = "Lolek";
            const string secondName = "Bolek";
            const string phoneNumber = "123456789";
            Client client = new Client(email, firstName, secondName, phoneNumber);

            Assert.Throws<ArgumentException>(() => dataRepository.GetClient(dataRepository.FindClient(client)));
        }

        #endregion


    }
}