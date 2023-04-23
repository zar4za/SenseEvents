using SenseEvents.Features.Tickets;

namespace SenseEvents.Features.Events;

public interface IEventsService
{
    Task<Guid> AddEvent(Event newEvent);

    Task<IEnumerable<Event>> GetEvents();

    Task<Event> GetEvent(Guid id);

    Task<bool> DeleteEvent(Guid id);

    Task<Ticket> AddTicket(Guid eventId, Ticket ticket);

    Task<bool> UpdateEvent(Event update);

    Task<bool> UpdateImage(Guid imageId, Guid? newId);

    Task<bool> DeleteEventsInSpace(Guid spaceId);
}