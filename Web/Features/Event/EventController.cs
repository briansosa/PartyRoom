using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Business;
using DTO;

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
        public List<DtoEventResponse> Get()
        {
            return eventService.Get();
        }

        [HttpPost]
        public int Post([FromBody] DtoEvent dtoEvent)
        {
            int id = eventService.Add(dtoEvent);
            return id;
        }
    }
}
