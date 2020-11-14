using System;
using BookStore.Model.Entities;

namespace BookStore.Model
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
                dataContext.CopyDetailses.Add(copyDetails);
            }


            for (int i = 0; i < invoiceNumber; i++)
            {
                Invoice invoice = new Invoice(
                    dataContext.Clients[random.Next(0, clientNumber)],
                    dataContext.CopyDetailses[random.Next(0, copyDetailsNumber)],
                    new DateTime(random.Next(2000, 2020), random.Next(1, 12), random.Next(1, 28))
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