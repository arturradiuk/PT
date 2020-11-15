using System;
using System.Runtime.CompilerServices;

namespace BookStore
{
    public class Reclamation : Event
    {
        public Invoice Invoice { get; set; }
        public bool isBookFaulty { get; set; }
        

        public Reclamation(DateTime eventDateTime, Invoice invoice, string description, bool isBookFaulty) : base(
            eventDateTime, description)
        {
            this.Invoice = invoice;
            this.isBookFaulty = isBookFaulty;
        }

        public Reclamation(DateTime eventDateTime, Invoice invoice, string description) : base(eventDateTime,
            description)
        {
            this.Invoice = invoice;
        }
    }
}