using Business.Config.Validation;
using Common.Guards;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Features.Event
{
    public class EventValidation : IValidatorModel
    {
        private DtoEventBasicRequest eventModel;
        private IConfigurationSection configuration;

        public EventValidation(DtoEventBasicRequest eventModel, IConfigurationSection configuration)
        {
            this.eventModel = eventModel;
            this.configuration = configuration;
        }

        public void ValidateModel()
        {
            var minHour = configuration.GetValue<int>("MinHour");
            var dateMinHour = Convert.ToDateTime(eventModel.DateStart.Date).AddHours(minHour);
            var maxHour = configuration.GetValue<int>("MaxHour");
            var dateMaxHour = maxHour >= 0 && maxHour < minHour
                ? Convert.ToDateTime(eventModel.DateStart.Date).AddDays(1).AddHours(maxHour)
                : Convert.ToDateTime(eventModel.DateStart.Date).AddHours(maxHour);
            var minDuration = configuration.GetValue<int>("MinDuration");
            var maxDuration = configuration.GetValue<int>("MaxDuration");
           
            Guard.Against.NullOrWhiteSpace(eventModel.Name, nameof(eventModel.Name));
            Guard.Against.EmptyOrNullDateTime(eventModel.DateStart, nameof(eventModel.DateStart));
            Guard.Against.EmptyOrNullDateTime(eventModel.DateFinish, nameof(eventModel.DateFinish));
            Guard.Against.Higher(eventModel.DateFinish, eventModel.DateStart, nameof(eventModel.DateFinish), nameof(eventModel.DateStart));
            Guard.Against.HigherOrEquals(eventModel.DateStart, dateMinHour, nameof(eventModel.DateStart), nameof(dateMinHour));
            Guard.Against.HigherOrEquals(dateMaxHour, eventModel.DateFinish, nameof(dateMaxHour), nameof(eventModel.DateFinish));
            Guard.Against.OutOfRange(eventModel.Duration.Hours, nameof(eventModel.Duration), minDuration, maxDuration);
        }
    }

    public class PutEventValidator : IValidatorModel
    {
        private DtoEventRequest eventModel;
        private EventValidation basicEventModel;
        public PutEventValidator(DtoEventRequest eventModel, IConfigurationSection configuration)
        {
            this.eventModel = eventModel;
            this.basicEventModel = new EventValidation(eventModel, configuration);
        }
        public void ValidateModel()
        {
            basicEventModel.ValidateModel();
            Guard.Against.Zero(eventModel.Id, nameof(eventModel.Id));
        }
    }

    public class DeleteEventValidator : IValidatorModel
    {
        private int id;
        public DeleteEventValidator(int id)
        {
            this.id = id;
        }
        public void ValidateModel()
        {
            Guard.Against.Zero(id, nameof(id));
        }
    }
}