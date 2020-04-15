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
using Web.Features;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : BaseController
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
            return Result(result);
        }

        [HttpPost]
        public ActionResult Post([FromBody] DtoEventBasicRequest dtoEvent)
        {
            var result = eventService.Add(dtoEvent);
            return Result(result);
        }

        [HttpPut]
        public ActionResult Put([FromBody] DtoEventRequest dtoEvent)
        {
            var result = eventService.Put(dtoEvent);
            return Result(result);
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] DtoEventRequest dtoEvent)
        {
            var result = eventService.Delete(dtoEvent.Id);
            return Result(result);
        }
    }
}
