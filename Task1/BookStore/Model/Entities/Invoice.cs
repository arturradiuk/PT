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

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Invoice i = obj as Invoice;
                if (i == null) return false;
                return (this.Client.Equals(i.Client)) &&
                       (this.CopyDetails.Equals(i.CopyDetails)) && (this.EventDateTime.Equals(i.EventDateTime))
                       && (this.Description.Equals(i.Description));
            }
        }
    }
}