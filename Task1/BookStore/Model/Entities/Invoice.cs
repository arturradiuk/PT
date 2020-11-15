using System;

namespace BookStore.Model.Entities
{
    public class Invoice : Event
    {
        public Client Client;

        public CopyDetails CopyDetails;

        public Invoice(Client client, CopyDetails copyDetails, DateTime eventDateTime, string description) : base(
            eventDateTime, description)
        {
            Client = client;
            CopyDetails = copyDetails;
            EventDateTime = eventDateTime;
        }
    }
}