using System.Collections.Generic;
using DatabaseInterface;
using Domain;
using System;

namespace DatabaseImplementation
{
    public class EventRepository : IEventRepository
    {
        public static List<Event> events = new List<Event>();

        public int Add(Event eventModel)
        {
            events.Add(eventModel);
            return 1;
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Event> Get()
        {
            return events;
        }

        public void Put(Event eventModel)
        {
            throw new System.NotImplementedException();
        }
    }
}