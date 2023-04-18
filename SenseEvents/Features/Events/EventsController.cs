using MediatR;
using Microsoft.AspNetCore.Authorization;
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
[Authorize]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<EventsController> _logger;

    public EventsController(ILogger<EventsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Получение списка мероприятий
    /// </summary>
    [HttpGet]
    [ProducesResponseType(statusCode: 200, type: typeof(GetEventsResponse))]
    [ProducesResponseType(statusCode: 400, type: typeof(ScError))]
    [ProducesResponseType(statusCode: 401)]
    public async Task<IActionResult> GetEvents()
    {
        _logger.LogInformation("Received GET /api/events");
        var events = await _mediator.Send(new GetEventsQuery());
        return Ok(events);
    }

    /// <summary>
    /// Добавление нового мероприятия
    /// </summary>
    /// <param name="command">Модель запроса</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(statusCode: 200, type: typeof(AddEventResponse))]
    [ProducesResponseType(statusCode: 400, type: typeof(ScError))]
    [ProducesResponseType(statusCode: 401)]
    public async Task<IActionResult> AddEvent([FromBody] AddEventCommand command)
    {
        _logger.LogInformation("Received POST /api/events");
        var eventId = await _mediator.Send(command);
        return Ok(eventId);
    }

    /// <summary>
    /// Изменение существующего мероприятия
    /// </summary>
    /// <param name="id">Guid мероприятия</param>
    /// <param name="command">Модель запроса</param>
    /// <returns></returns>
    // ReSharper disable once RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute
    // Not all methods need an id
    [HttpPut("{id:guid}")]
    [ProducesResponseType(statusCode: 200, type: typeof(UpdateEventResponse))]
    [ProducesResponseType(statusCode: 400, type: typeof(ScError))]
    [ProducesResponseType(statusCode: 401)]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] UpdateEventCommand command)
    {
        command.Id = id;
        _logger.LogInformation("Received PUT /api/events");
        var success = await _mediator.Send(command);
        return Ok(success);
    }

    /// <summary>
    /// Удаление мероприятия
    /// </summary>
    // ReSharper disable once RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute
    // Not all methods need an id
    // ReSharper disable once RouteTemplates.RouteParameterIsNotPassedToMethod
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(statusCode: 200, type: typeof(DeleteEventResponse))]
    [ProducesResponseType(statusCode: 400, type: typeof(ScError))]
    [ProducesResponseType(statusCode: 401)]
    // ReSharper disable once RouteTemplates.MethodMissingRouteParameters
    public async Task<IActionResult> DeleteEvent([FromQuery] DeleteEventCommand command)
    {
        _logger.LogInformation("Received DELETE /api/events");
        var success = await _mediator.Send(command);
        return Ok(success);
    }

    /// <summary>
    /// Получение выданных билетов на мероприятие
    /// </summary>
    // ReSharper disable once RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute
    //      Not all methods need an id
    // ReSharper disable once RouteTemplates.RouteParameterIsNotPassedToMethod
    //      id is passed to GetTicketsQuery.EventId
    [HttpGet("{id:guid}/tickets")]
    [ProducesResponseType(statusCode: 200, type: typeof(GetTicketsResponse))]
    [ProducesResponseType(statusCode: 400, type: typeof(ScError))]
    [ProducesResponseType(statusCode: 401)]
    // ReSharper disable once RouteTemplates.MethodMissingRouteParameters
    public async Task<IActionResult> GetTickets([FromQuery] GetTicketsQuery query)
    {
        _logger.LogInformation($"Received GET /api/events/{query.EventId}/tickets");
        var tickets = await _mediator.Send(query);
        return Ok(tickets);
    }

    /// <summary>
    /// Выдача билета на мероприятие
    /// </summary>
    /// <param name="id">Guid мероприятия</param>
    /// <param name="command">Модель запроса</param>
    /// <returns></returns>
    // ReSharper disable once RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute
    // Not all methods need an id
    [HttpPost("{id:guid}/tickets")]
    [ProducesResponseType(statusCode: 200, type: typeof(AddTicketResponse))]
    [ProducesResponseType(statusCode: 400, type: typeof(ScError))]
    [ProducesResponseType(statusCode: 401)]
    public async Task<IActionResult> AddTicket(Guid id, AddTicketCommand command)
    {
        command.EventId = id;
        _logger.LogInformation($"Received GET /api/events/{id}/tickets");
        var ticket = await _mediator.Send(command);
        return Ok(ticket);
    }
}