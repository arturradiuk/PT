using System;
using BookStore.Model;
using BookStore.Model.Entities;

namespace BookStoreTest
{
    public class RandomDataFiller : IDataFiller
    {
        public void Fill(DataContext dataContext)
        {
            int clientNumber = 5;
            int bookNumber = 5;
            int copyDetailsNumber = 5;
            int invoiceNumber = 5;

            Random random = new Random();

            for (int i = 0; i < clientNumber; i++)
            {
                Client client = new Client(
                    GenerateRandomString(8) + "@" + GenerateRandomString(3) + ".com",
                    GenerateRandomString(5),
                    GenerateRandomString(5),
                    GenerateNumberString(8));
                dataContext.Clients.Add(client);
            }

            for (int i = 0; i < bookNumber; i++)
            {
                Book book = new Book(GenerateRandomString(8),
                    GenerateRandomString(10),
                    random.Next(1900, 2020)
                );
                dataContext.Books.Add(i, book);
            }

            for (int i = 0; i < copyDetailsNumber; i++)
            {
               CopyDetails copyDetails = new CopyDetails(
                    dataContext.Books[random.Next(0, bookNumber)],
                    (decimal) random.NextDouble(),
                    (decimal) random.NextDouble(),
                    random.Next(),
                    GenerateRandomString(5)
                );
                dataContext.CopyDetailses.Add(copyDetails);
            }

            for (int i = 0; i < invoiceNumber; i++)
            {
                Invoice invoice = new Invoice(
                    dataContext.Clients[random.Next(0, clientNumber)],
                    dataContext.CopyDetailses[random.Next(0,copyDetailsNumber)],
                    new DateTime(random.Next(2000,2020),random.Next(1,12),random.Next(1,28))
                );
                dataContext.Invoices.Add(invoice);
            }
        }

        private String GenerateRandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        private String GenerateNumberString(int length)
        {
            var chars = "0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
    }
}