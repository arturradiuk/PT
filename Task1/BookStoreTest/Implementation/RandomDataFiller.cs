using System;
using BookStore.Model;
using BookStore.Model.Entities;

namespace BookStoreTest.Implementation
{
    public class RandomDataFiller : IDataFiller
    {
        private int clientNumber;
        private int bookNumber;
        private int copyDetailsNumber;
        private int invoiceNumber;

        public RandomDataFiller(int bookNumber, int invoiceNumber, int clientNumber)
        {
            this.clientNumber = clientNumber;
            this.bookNumber = bookNumber;
            this.invoiceNumber = invoiceNumber;
            this.copyDetailsNumber = bookNumber;
        }

        public void Fill(DataContext dataContext)
        {
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
                CopyDetails copyDetails = new CopyDetails(
                    book,
                    (decimal) random.NextDouble(),
                    (decimal) random.NextDouble(),
                    random.Next(),
                    GenerateRandomString(5)
                );
                dataContext.AllCopyDetails.Add(copyDetails);
            }


            for (int i = 0; i < invoiceNumber; i++)
            {
                Invoice invoice = new Invoice(
                    dataContext.Clients[random.Next(0, clientNumber)],
                    dataContext.AllCopyDetails[random.Next(0, copyDetailsNumber)],
                    new DateTime(random.Next(2000, 2020), random.Next(1, 12), random.Next(1, 28)),
                    GenerateRandomString(8)
                );
                dataContext.Events.Add(invoice);
            }
        }

        private String GenerateRandomString(int length)
        {
            String chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] stringChars = new char[length];
            Random random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            string finalString = new String(stringChars);
            return finalString;
        }

        private String GenerateNumberString(int length)
        {
            string chars = "0123456789";
            char[] stringChars = new char[length];
            Random random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            string finalString = new String(stringChars);
            return finalString;
        }
    }
}