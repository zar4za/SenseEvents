using SenseEvents.Features.Events.AddEvent;
using SenseEvents.Features.Events.DeleteEvent;

namespace SenseEvents.Features.Events
{
    public interface IEventsService
    {
        Task<Guid> AddEvent(AddEventCommand command);

        Task<IEnumerable<Event>> GetEvents();

        Task<Event?> GetEvent(Guid id);

        Task<bool> DeleteEvent(DeleteEventCommand command);
    }
}
