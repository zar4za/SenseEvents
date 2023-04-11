using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SenseEvents.Features.Events.AddEvent;
using SenseEvents.Features.Events.DeleteEvent;
using SenseEvents.Infrastructure.Identity;

namespace SenseEvents.Features.Events
{
    public class EventsService : IEventsService
    {
        private readonly IGuidService _guidService;
        private readonly IMongoCollection<Event> _events;

        public EventsService(IOptions<EventsMongoOptions> options, IGuidService guidService)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _events = client.GetDatabase(options.Value.DatabaseName)
                .GetCollection<Event>(options.Value.EventsCollectionName);

            _guidService = guidService;
        }


        public async Task<Guid> AddEvent(Event newEvent)
        {
            newEvent.Id = _guidService.GetNewId();

            await _events.InsertOneAsync(newEvent);
            return newEvent.Id;
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await Task.FromResult(_events.AsQueryable());
        }

        public async Task<Event> GetEvent(Guid id)
        {
            var cursor = await _events.FindAsync(x => x.Id == id);
            return await cursor.SingleAsync();
        }

        public async Task<bool> DeleteEvent(Guid id)
        {
            var deleted = await _events.FindOneAndDeleteAsync(x => x.Id == id);
            return deleted != null;
        }

        public async Task<Ticket> AddTicket(Guid eventId, Ticket ticket)
        {
            var cursor = await _events.FindAsync(x => x.Id == eventId);
            var updateEvent = await cursor.SingleAsync();
            updateEvent.AddTicket(ticket);
            return ticket;
        }
    }
}
