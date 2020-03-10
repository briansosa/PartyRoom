using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Contracts;
using DTO;

namespace Business
{
    public class EventService : IEventService
    {
        private IEventRepository repository;
        public EventService(IEventRepository repo)
        {
            this.repository = repo;
        }

        public int Add(DtoEventRequest dtoEvent){
            var eventData = new Event{
                Name = dtoEvent.Name,
                DateStart = dtoEvent.DateStart,
                DateFinish = dtoEvent.DateFinish
            };
            int id = this.repository.Add(eventData);
            return id;
        }

        public void Delete(int id)
        {
            this.repository.Delete(id);
        }

        public int Put(DtoEventRequest eventModel)
        {
            var eventData = new Event {
                Id = eventModel.Id,
                Name = eventModel.Name,
                DateStart = eventModel.DateStart,
                DateFinish = eventModel.DateFinish
            };
            return this.repository.Put(eventData);
        }

        public List<DtoEventResponse> Get()
        {
            var events = this.repository.GetAll();
            var eventsResponse = events.Select(eventElem =>
                new DtoEventResponse 
                {
                    Name = eventElem.Name,
                    DateStart = eventElem.DateStart,
                    DateFinish = eventElem.DateFinish
                }).ToList();
            return eventsResponse;
        }
    }
}
