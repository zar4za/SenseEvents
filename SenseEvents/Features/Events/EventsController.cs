using MediatR;
using Microsoft.AspNetCore.Mvc;
using SenseEvents.Features.Events.AddEvent;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] UpdateEventRequest request)
        {
            var command = new UpdateEventCommand()
            {
                Id = id,
                StartUtc = request.StartUtc,
                EndUtc = request.EndUtc,
                Name = request.Name,
                Description = request.Description,
                ImageId = request.ImageId,
                SpaceId = request.SpaceId
            };

            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
