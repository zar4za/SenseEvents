using SenseEvents.Features.Events.AddEvent;
using SenseEvents.Features.Events.DeleteEvent;
using SenseEvents.Infrastructure.Identity;

namespace SenseEvents.Features.Events
{
    public class EventsServiceMock : IEventsService
    {
        private readonly List<Event> _events = new();
        private readonly IGuidService _guidService;

        public EventsServiceMock(IGuidService guidService)
        {
            _guidService = guidService;
        }

        public async Task<Guid> AddEvent(AddEventCommand command)
        {
            var id = _guidService.GetNewId();

            await Task.Run(() =>
            { 
                _events.Add(new Event()
                {
                    Id = id,
                    Name = command.Name,
                    Description = command.Description,
                    StartUtc = command.StartUtc,
                    EndUtc = command.EndUtc,
                    ImageId = command.ImageId,
                    SpaceId = command.SpaceId,
                    Tickets = new List<Ticket>()
                });
            });

            return id;
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await Task.Run(() => _events.AsEnumerable());
        }

        public async Task<Event> GetEvent(Guid id)
        {
            return await Task.Run(() => _events.FirstOrDefault(e => e.Id == id)) ?? 
                   throw new InvalidOperationException($"No event with id {id}");
        }

        public async Task<bool> DeleteEvent(DeleteEventCommand command)
        {
            return await Task.Run(() =>
            {
                return _events.RemoveAll(e => e.Id == command.Id) > 0;
            });
        }

        public async Task<Ticket> AddTicket(Guid eventId, Ticket ticket)
        {
            var plannedEvent = await GetEvent(eventId);
            plannedEvent.AddTicket(ticket);

            return ticket;
        }
    }
}
