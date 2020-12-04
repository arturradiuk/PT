using System;
using System.IO;
using Xunit;
using BookStore.Model;
using Serializer;

namespace SerializerTest
{
    public class SerializerTest
    {
        [Fact]
        public void InvalidSerializedFileTest()
        {
            Assert.Throws<FileLoadException>(() => BinarySerializer.DeserializeFromBinary("I don't exist"));
        }
        
        [Fact]
        public void BookSerializationTest()
        {
            Book book = new Book("Why should you delete system32?", "Micheal Phealps", 2015);
            
            string filePath = "serializedBook";
            BinarySerializer.SerializeToBinary(book, filePath);
            object deserializedBook = BinarySerializer.DeserializeFromBinary(filePath);
            
            Assert.Equal(book, deserializedBook);
        }

        [Fact]
        public void ClientSerializationTest()
        {
            Client client = new Client("michał.jajko@gmail.com",
                "Michał", "Jajko", "123-565-789");
            
            string filePath = "serializedClient";
            BinarySerializer.SerializeToBinary(client, filePath);
            object deserializedClient = BinarySerializer.DeserializeFromBinary(filePath);
            
            Assert.Equal(client, deserializedClient);
        }

        [Fact]
        public void CopyDetailsSerializationTest()
        {
            Book book = new Book("Why should you delete system32?", "Micheal Phealps", 2015);
            Client client = new Client("michał.jajko@gmail.com",
                "Michał", "Jajko", "123-565-789");
            CopyDetails copyDetails = new CopyDetails(book, 15, 1, 2, "sample copy details");
            
            string filePath = "serializedCopyDetails";
            BinarySerializer.SerializeToBinary(copyDetails, filePath);
            object deserializedCopyDetails = BinarySerializer.DeserializeFromBinary(filePath);
            
            Assert.Equal(copyDetails, deserializedCopyDetails);
        }
        
        
        [Fact]
        public void InvoiceSerializationTest()
        {
            Book book = new Book("Why should you delete system32?", "Micheal Phealps", 2015);
            Client client = new Client("michał.jajko@gmail.com",
                "Michał", "Jajko", "123-565-789");
            CopyDetails copyDetails = new CopyDetails(book, 15, 1, 2, "sample copy details");
            Invoice invoice = new Invoice(client, copyDetails, DateTime.Now, "sample invoice");

            string filePath = "serializedInvoice";
            BinarySerializer.SerializeToBinary(invoice, filePath);
            object deserializedInvoice = BinarySerializer.DeserializeFromBinary(filePath);
            
            Assert.Equal(invoice, deserializedInvoice);
        }

        [Fact]
        public void ReclamationSerializationTest()
        {
            Book book = new Book("Why should you delete system32?", "Micheal Phealps", 2015);
            Client client = new Client("michał.jajko@gmail.com",
                "Michał", "Jajko", "123-565-789");
            CopyDetails copyDetails = new CopyDetails(book, 15, 1, 2, "sample copy details");
            Invoice invoice = new Invoice(client, copyDetails, DateTime.Now, "sample invoice");
            Reclamation reclamation = new Reclamation(DateTime.Now, invoice, "sample reclamation");

            string filePath = "serializedReclamation";
            BinarySerializer.SerializeToBinary(reclamation, filePath);
            object deserializedReclamation = BinarySerializer.DeserializeFromBinary(filePath);

            Assert.Equal(reclamation, deserializedReclamation);
        }
    }
}