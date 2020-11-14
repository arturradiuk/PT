using System;

namespace BookStore
{
    public class Reclamation : Event
    {
        public Invoice Invoice { get; set; }

        public Reclamation(Client client, CopyDetails copyDetails, DateTime eventDateTime, Invoice invoice) : base(
            client, copyDetails, eventDateTime)
        {
            this.Invoice = invoice;
        }
    }
}