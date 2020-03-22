using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Business;
using DTO;
using Common.Guards;
using Common.Functional;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private IEventService eventService;

        public EventController(ILogger<EventController> logger, IEventService _event)
        {
            _logger = logger;
            eventService = _event;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = eventService.Get();
            if (result.IsSuccess) return Ok(result.Value);
            else return BadRequest(result.ErrorMessage);


        }

        [HttpPost]
        public int Post([FromBody] DtoEventRequest dtoEvent)
        {
            Guard.Against.NullOrWhiteSpace(dtoEvent.Name, nameof(dtoEvent.Name));
            Guard.Against.EmptyOrNullDateTime(dtoEvent.DateStart, nameof(dtoEvent.DateStart));
            Guard.Against.EmptyOrNullDateTime(dtoEvent.DateFinish, nameof(dtoEvent.DateFinish));
            int id = eventService.Add(dtoEvent);
            return id;
        }

        [HttpPut]
        public ActionResult Put([FromBody] DtoEventRequest dtoEvent)
        {
            Guard.Against.Zero(dtoEvent.Id, nameof(dtoEvent.Id));
            Guard.Against.NullOrWhiteSpace(dtoEvent.Name, nameof(dtoEvent.Name));
            Guard.Against.EmptyOrNullDateTime(dtoEvent.DateStart, nameof(dtoEvent.DateStart));
            Guard.Against.EmptyOrNullDateTime(dtoEvent.DateFinish, nameof(dtoEvent.DateFinish));
            var result = eventService.Put(dtoEvent);
            if (result.IsSuccess) return Ok(result.Value);
            else return BadRequest(result.ErrorMessage);

        }

        [HttpDelete]
        public ActionResult Delete([FromBody] DtoEventRequest dtoEvent)
        {
            Guard.Against.Zero(dtoEvent.Id, nameof(dtoEvent.Id));
            var result = eventService.Delete(dtoEvent.Id);
            if (result.IsSuccess) return Ok();
            else return BadRequest(result.ErrorMessage);
        }
    }
}
