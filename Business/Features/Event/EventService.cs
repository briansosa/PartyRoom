using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Contracts;
using DTO;
using Common.Functional;

namespace Business
{
    public class EventService : IEventService
    {
        private IEventRepository repository;
        public EventService(IEventRepository repo)
        {
            this.repository = repo;
        }

        public int Add(DtoEventRequest dtoEvent)
        {
            var eventData = new Event
            {
                Name = dtoEvent.Name,
                DateStart = dtoEvent.DateStart,
                DateFinish = dtoEvent.DateFinish
            };
            int id = this.repository.Add(eventData);
            return id;
        }

        public Result Delete(int id)
        {
            var result = this.repository.Delete(id);
            return result;
        }

        public Result<int> Put(DtoEventRequest eventModel)
        {
            var eventData = new Event
            {
                Id = eventModel.Id,
                Name = eventModel.Name,
                DateStart = eventModel.DateStart,
                DateFinish = eventModel.DateFinish
            };
            return this.repository.Put(eventData);
        }

        public Result<List<DtoEventResponse>> Get()
        {
            var resultDb = this.repository.GetAll();
            if (resultDb.IsSuccess)
            {
                var events = resultDb.Value;
                var eventsResponse = events.Select(eventElem =>
                new DtoEventResponse
                {
                    Name = eventElem.Name,
                    DateStart = eventElem.DateStart,
                    DateFinish = eventElem.DateFinish
                }).ToList();
                return Result.SuccessWithReturnValue(eventsResponse);
            }
            return Result.FailWithDefaultReturnValue<List<DtoEventResponse>>(resultDb.ErrorMessage);
        }
    }
}
