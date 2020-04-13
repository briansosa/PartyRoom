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
        private IConfigurationSection configuration;

        public EventService(IEventRepository repository, IValidator validator, IConfiguration configuration)
        {
            this.repository = repository;
            this.validator = validator;
            this.configuration = configuration.GetSection("EventValidation");
        }

        public Result<int> Add(DtoEventBasicRequest dtoEvent)
        {
            validator.Validate(new EventValidation(dtoEvent, configuration));
            // obtener los eventos que entre esas dos fechas y horas, pero al dateFinish sumarle 30 minutos.
            // que es el tiempo muerto
            var sleepTime = configuration.GetValue<int>("SleepTime");
            var dateFinish = Convert.ToDateTime(dtoEvent.DateFinish.AddMinutes(sleepTime));
            var listOfEventInDates = GetBetweenDateTime(dtoEvent.DateStart, dateFinish);
            if(listOfEventInDates.IsFailure)
            {
                var eventData = new Event
                {
                    Name = dtoEvent.Name,
                    DateStart = dtoEvent.DateStart,
                    DateFinish = dtoEvent.DateFinish
                };
                int id = this.repository.Add(eventData);
                return Result.SuccessWithReturnValue(id);
            }
            return Result.FailWithDefaultReturnValue<int>("There is no place available between those dates");
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
        public Result<DtoEventResponse> GetById(int id)
        {
            var resultDb = this.repository.GetById(id);
            if (resultDb.IsSuccess)
            {
                var events = resultDb.Value;
                var eventsResponse = new DtoEventResponse
                {
                    Name = events.Name,
                    DateStart = events.DateStart,
                    DateFinish = events.DateFinish
                };
                return Result.SuccessWithReturnValue(eventsResponse);
            }
            return Result.FailWithDefaultReturnValue<DtoEventResponse>(resultDb.ErrorMessage);
        }

        private Result<List<Event>> GetBetweenDateTime(DateTime rangeFrom, DateTime rangeTo)
        {
            Func<Event, bool> GetByDates = x => x.DateFinish.CompareTo(rangeFrom) >= 0 && x.DateStart.CompareTo(rangeTo) <= 0;
            var data = this.repository.GetByFilter(GetByDates);
            return data;
        }
    }
}
