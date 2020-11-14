using System;

namespace BookStore
{
    public abstract class Event
    {
        public Client Client;

        public CopyDetails CopyDetails;
        public DateTime EventDateTime { get; set; }

        public Event(Client client, CopyDetails copyDetails, DateTime eventDateTime)
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
                Invoice i = (Invoice) obj;
                return (this.Client.Equals(i.Client)) &&
                       (this.CopyDetails.Equals(i.CopyDetails)) && (this.EventDateTime.Equals(i.EventDateTime));
            }
        }

        public override string ToString()
        {
            return "Event: " + Client + CopyDetails + "EventDateTime: " + EventDateTime + "; ";
        }
    }
}