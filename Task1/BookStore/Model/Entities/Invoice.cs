using System;

namespace BookStore
{
    public class Invoice: Event
    {
        public Invoice(Client client, CopyDetails copyDetails, DateTime eventDateTime) :base(client, copyDetails, eventDateTime)
        {
            Client = client;
            CopyDetails = copyDetails;
            EventDateTime = eventDateTime;
        }
    }
}