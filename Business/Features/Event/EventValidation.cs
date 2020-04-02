using Business.Contracts;
using Common.Guards;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Features.Event
{
    public class EventValidation : IValidatorModel
    {
        private DtoEventBasicRequest eventModel;

        public EventValidation(DtoEventBasicRequest eventModel)
        {
            this.eventModel = eventModel;
        }

        public void ValidateModel()
        {
            Guard.Against.NullOrWhiteSpace(eventModel.Name, nameof(eventModel.Name));
            Guard.Against.EmptyOrNullDateTime(eventModel.DateStart, nameof(eventModel.DateStart));
            Guard.Against.EmptyOrNullDateTime(eventModel.DateFinish, nameof(eventModel.DateFinish));
        }
    }

    public class PutEventValidator : IValidatorModel
    {
        private DtoEventRequest eventModel;
        private EventValidation basicEventModel;
        public PutEventValidator(DtoEventRequest eventModel)
        {
            this.eventModel = eventModel;
            this.basicEventModel = new EventValidation(eventModel);
        }
        public void ValidateModel()
        {
            basicEventModel.ValidateModel();
            Guard.Against.Zero(eventModel.Id, nameof(eventModel.Id));
        }
    }
}
