using SenseEvents.Features.Events.AddEvent;
using SenseEvents.Features.Events.DeleteEvent;

namespace SenseEvents.Features.Events;

public interface IEventsService
{
    Task<Guid> AddEvent(Event newEvent);

    Task<IEnumerable<Event>> GetEvents();

    Task<Event> GetEvent(Guid id);

    Task<bool> DeleteEvent(DeleteEventCommand command);

    Task<Ticket> AddTicket(Guid eventId, Ticket ticket);
}