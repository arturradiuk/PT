using System;

namespace BookStore
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public Client Client;
        public CopyDetails CopyDetails;
        public DateTimeOffset PurchaseTime { get; set; }
        

    }
}