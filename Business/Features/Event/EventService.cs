using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseInterface;
using Domain;
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

        public int Add(DtoEvent dtoEvent){
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

        public void Put(DtoEvent eventModel)
        {
            var eventData = new Event {
                // ACA DEBERIA IR EL ID? EN EL DOMINIO SE PONE EL ID?
                //Id = eventModel.Id,
                DateStart = eventModel.DateStart,
                DateFinish = eventModel.DateFinish
            };
            this.repository.Put(eventData);
        }

        public List<DtoEventResponse> Get()
        {
            var events = this.repository.Get();
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
