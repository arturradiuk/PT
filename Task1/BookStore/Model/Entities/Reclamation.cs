using System;

namespace BookStore.Model.Entities
{
    public class Reclamation : Event
    {
        public Invoice Invoice { get; set; }
        public bool IsBookFaulty { get; set; }


        public Reclamation(DateTime eventDateTime, Invoice invoice, string description, bool isBookFaulty) : base(
            eventDateTime, description)
        {
            this.Invoice = invoice;
            this.IsBookFaulty = isBookFaulty;
        }

        public Reclamation(DateTime eventDateTime, Invoice invoice, string description) : base(eventDateTime,
            description)
        {
            this.Invoice = invoice;
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Reclamation r = obj as Reclamation;
                if (r == null) return false;
                return (this.Invoice.Equals(r.Invoice)) &&
                       (this.IsBookFaulty.Equals(r.IsBookFaulty)) && (this.Description.Equals(r.Description)) &&
                       (this.EventDateTime.Equals(r.EventDateTime));
            }
        }
    }
}