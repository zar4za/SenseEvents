using MediatR;
using Microsoft.AspNetCore.Mvc;
using SenseEvents.Features.Events.AddEvent;
using SenseEvents.Features.Events.DeleteEvent;
using SenseEvents.Features.Events.GetEvents;
using SenseEvents.Features.Events.UpdateEvent;
using SenseEvents.Infrastructure.Validation;

namespace SenseEvents.Features.Events
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(GetEventsResponse))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _mediator.Send(new GetEventsQuery());
            return Ok(events);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(AddEventResponse))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> AddEvent([FromBody] AddEventCommand command)
        {
            var eventId = await _mediator.Send(command);
            return Ok(eventId);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(UpdateEventResponse))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] UpdateEventCommand command)
        {
            command.Id = id;
            var success = await _mediator.Send(command);
            return Ok(success);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(DeleteEventResponse))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var command = new DeleteEventCommand()
            {
                Id = id
            };

            var success = await _mediator.Send(command);
            return Ok(success);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTickets([FromRoute] GetEventsQuery query)
        {
            var tickets = await _mediator.Send(query);
            return Ok(tickets);
        }
    }
}
