using System;

namespace BookStore
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public Client Client;
        public CopyDetails CopyDetails;
        public DateTimeOffset PurchaseTime { get; set; }

        public Invoice(Client client, CopyDetails copyDetails, Guid id, DateTimeOffset purchaseTime)
        {
            Client = client;
            CopyDetails = copyDetails;
            Id = id;
            PurchaseTime = purchaseTime;
        }

        // todo 
        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Invoice i = (Invoice) obj;
                return (this.Id.Equals(i.Id)) && (this.Client.Equals(i.Client)) &&
                       (this.CopyDetails.Equals(i.CopyDetails)) && (this.PurchaseTime.Equals(i.PurchaseTime));
            }
        }
    }
}