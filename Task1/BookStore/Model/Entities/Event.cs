using System;

namespace BookStore.Model
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
    }
}