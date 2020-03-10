using System.Collections.Generic;
using Domain.Entities;
using System;
using Domain.Contracts;
using System.Linq;

namespace Database.Implementations
{
    public class EventRepository : IEventRepository
    {
        private PartyRoomContext context;

        public EventRepository(PartyRoomContext _context)
        {
            context = _context;
        }
        public int Add(Event eventModel)
        {
            context.Add(eventModel);
            var id = context.SaveChanges();
            return id;
        }

        public void Delete(int id)
        {
            var eventDb = context.Set<Event>().FirstOrDefault(e => e.Id == id);
            context.Delete(eventDb);
            context.SaveChanges();

        }

        public List<Event> GetAll()
        {
            var events = context.Set<Event>().ToList();
            return events;
        }

        public int Put(Event eventModel)
        {
            context.Update<Event>(eventModel);
            var id = context.SaveChanges();
            return id;
        }
    }
}