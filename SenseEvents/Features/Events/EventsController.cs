using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;
using SenseEvents.Features.Events.AddEvent;
using SenseEvents.Features.Events.AddTicket;
using SenseEvents.Features.Events.DeleteEvent;
using SenseEvents.Features.Events.GetEvents;
using SenseEvents.Features.Events.GetTickets;
using SenseEvents.Features.Events.UpdateEvent;

namespace SenseEvents.Features.Events;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получение списка мероприятий
    /// </summary>
    [HttpGet]
    [ProducesResponseType(statusCode: 200, type: typeof(GetEventsResponse))]
    [ProducesResponseType(statusCode: 400, type: typeof(ScError))]
    [ProducesResponseType(statusCode: 500, type: typeof(ScError))]
    public async Task<IActionResult> GetEvents()
    {
        var events = await _mediator.Send(new GetEventsQuery());
        return Ok(events);
    }

    [HttpPost]
    [ProducesResponseType(statusCode: 200, type: typeof(AddEventResponse))]
    [ProducesResponseType(statusCode: 400, type: typeof(ScError))]
    [ProducesResponseType(statusCode: 500, type: typeof(ScError))]
    public async Task<IActionResult> AddEvent([FromBody] AddEventCommand command)
    {
        var eventId = await _mediator.Send(command);
        return Ok(eventId);
    }

        
    // ReSharper disable once RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute
    // Not all methods need an id
    [HttpPut("{id:guid}")]
    [ProducesResponseType(statusCode: 200, type: typeof(UpdateEventResponse))]
    [ProducesResponseType(statusCode: 400, type: typeof(ScError))]
    [ProducesResponseType(statusCode: 500, type: typeof(ScError))]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] UpdateEventCommand command)
    {
        command.Id = id;
        var success = await _mediator.Send(command);
        return Ok(success);
    }

    // ReSharper disable once RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute
    // Not all methods need an id
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(statusCode: 200, type: typeof(DeleteEventResponse))]
    [ProducesResponseType(statusCode: 400, type: typeof(ScError))]
    [ProducesResponseType(statusCode: 500, type: typeof(ScError))]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        var command = new DeleteEventCommand
        {
            Id = id
        };

        var success = await _mediator.Send(command);
        return Ok(success);
    }

    // ReSharper disable once RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute
    // Not all methods need an id
    [HttpGet("{id:guid}/tickets")]
    [ProducesResponseType(statusCode: 200, type: typeof(GetTicketsResponse))]
    [ProducesResponseType(statusCode: 400, type: typeof(ScError))]
    [ProducesResponseType(statusCode: 500, type: typeof(ScError))]
    public async Task<IActionResult> GetTickets(Guid id)
    {
        var query = new GetTicketsQuery
        {
            EventId = id
        };

        var tickets = await _mediator.Send(query);
        return Ok(tickets);
    }

    // ReSharper disable once RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute
    // Not all methods need an id
    [HttpPost("{id:guid}/tickets")]
    [ProducesResponseType(statusCode: 200, type: typeof(AddTicketResponse))]
    [ProducesResponseType(statusCode: 400, type: typeof(ScError))]
    [ProducesResponseType(statusCode: 500, type: typeof(ScError))]
    public async Task<IActionResult> AddTicket(Guid id, AddTicketCommand command)
    {
        command.EventId = id;
        var ticket = await _mediator.Send(command);
        return Ok(ticket);
    }
}