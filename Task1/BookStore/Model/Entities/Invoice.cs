using System;

namespace BookStore
{
    public class Invoice
    {

        public Client Client;
        
        public CopyDetails CopyDetails;
        public DateTime PurchaseTime { get; set; }
        

        public Invoice(Client client, CopyDetails copyDetails, DateTime purchaseTime)
        {
            Client = client;
            CopyDetails = copyDetails;
            PurchaseTime = purchaseTime;
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
                       (this.CopyDetails.Equals(i.CopyDetails)) && (this.PurchaseTime.Equals(i.PurchaseTime));
            }
        }
    }
}