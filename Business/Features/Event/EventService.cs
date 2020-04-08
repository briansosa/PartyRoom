using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Contracts;
using DTO;
using Common.Functional;
using Business.Features.Event;
using Business.Config.Validation;
using Microsoft.Extensions.Configuration;

namespace Business
{
    public class EventService : IEventService
    {
        private IEventRepository repository;
        private IValidator validator;
        private IConfiguration configuration;

        public EventService(IEventRepository repository, IValidator validator, IConfiguration configuration)
        {
            this.repository = repository;
            this.validator = validator;
            this.configuration = configuration;
        }

        public int Add(DtoEventBasicRequest dtoEvent)
        {
            validator.Validate(new EventValidation(dtoEvent, configuration));
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
            validator.Validate(new DeleteEventValidator(id));
            var result = this.repository.Delete(id);
            return result;
        }

        public Result<int> Put(DtoEventRequest eventModel)
        {
            validator.Validate(new PutEventValidator(eventModel, configuration));
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
