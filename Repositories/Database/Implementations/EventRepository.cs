using System.Collections.Generic;
using Domain.Entities;
using System;
using Domain.Contracts;
using System.Linq;
using Common.Functional;

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
            return context.SaveChanges();
        }
        public Result Delete(int id)
        {
            Maybe<Event> eventDb = context.Set<Event>().FirstOrDefault(e => e.Id == id);
            if (eventDb.HasValue)
            {
                context.Delete(eventDb.Value);
                context.SaveChanges();
                return Result.Success();
            }
            else
                return Result.Fail("Entity not found");

        }
        public Result<List<Event>> GetAll()
        {
            var events = context.Set<Event>().ToList();
            if (events.Count > 0) return Result.SuccessWithReturnValue(events);
            else return Result.FailWithDefaultReturnValue<List<Event>>("No records found");
        }
        public Result<Event> GetById(int id)
        {
            Maybe<Event> @event = context.Set<Event>().FirstOrDefault(e => e.Id == id);
            if (@event.HasValue) return Result.SuccessWithReturnValue(@event.Value);
            else return Result.FailWithDefaultReturnValue<Event>("No record found");

        }
        public Result<List<Event>> GetByFilter(Func<Event, bool> filter)
        {
            var events = context.Set<Event>().Where(filter).ToList();
            if (events.Count > 0) return Result.SuccessWithReturnValue(events);
            else return Result.FailWithDefaultReturnValue<List<Event>>("No records found");
        }
        public Result<int> Put(Event eventModel)
        {
            Maybe<Event> maybeEvent = context.Set<Event>().FirstOrDefault(e => e.Id == eventModel.Id);
            if (maybeEvent.HasValue)
            {
                context.Entry(maybeEvent.Value).CurrentValues.SetValues(eventModel);
                var id = context.SaveChanges();
                return Result.SuccessWithReturnValue<int>(id);
            }
            else
                return Result.FailWithDefaultReturnValue<int>("Entity not found");
        }

    }
}