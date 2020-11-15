using System;

namespace BookStore
{
    public abstract class Event
    {
        public DateTime EventDateTime { get; set; }
        
        public string Description { get; set; }
        public Event(DateTime eventDateTime, string description)
        {
            EventDateTime = eventDateTime;
            Description = description;
        }

        // public override bool Equals(object? obj)
        // {
            // if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            // {
                // return false;
            // }
            // else
            // {
                // Invoice i = (Invoice) obj;
                // return (this.Client.Equals(i.Client)) &&
                       // (this.CopyDetails.Equals(i.CopyDetails)) && (this.EventDateTime.Equals(i.EventDateTime));
            // }
        // }

        // public override string ToString()
        // {
            // return "Event: " + Client + CopyDetails + "EventDateTime: " + EventDateTime + "; ";
        // }
        
        
    }
}