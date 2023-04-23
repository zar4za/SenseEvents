using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SenseEvents.Features.Tickets;
using SenseEvents.Infrastructure.Identity;

namespace SenseEvents.Features.Events;

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
        var filter = Builders<Event>.Filter.Eq("Id", eventId);
        var cursor = await _events.FindAsync(filter);
        var planned = await cursor.FirstAsync();

        if (planned?.CanIssueTicket == false)
            throw new InvalidOperationException("Reached ticket limit");

        var seat = planned!.Tickets.Count() + 1;
        ticket.Seat = seat;

        var update = Builders<Event>.Update.AddToSet("Tickets", ticket);
        await _events.FindOneAndUpdateAsync(filter, update);
        return ticket;
    }

    public async Task<bool> UpdateEvent(Event eventUpdate)
    {
        var update = Builders<Event>.Update
            .Set(x => x.Name, eventUpdate.Name)
            .Set(x => x.Description, eventUpdate.Description)
            .Set(x => x.StartUtc, eventUpdate.StartUtc)
            .Set(x => x.EndUtc, eventUpdate.EndUtc)
            .Set(x => x.ImageId, eventUpdate.ImageId)
            .Set(x => x.SpaceId, eventUpdate.SpaceId);

        var filter = Builders<Event>.Filter.Eq(x => x.Id, eventUpdate.Id);

        var result = await _events.UpdateOneAsync(filter, update);
        return result.IsAcknowledged;
    }

    public async Task<bool> UpdateImage(Guid imageId, Guid? newId)
    {
        var update = Builders<Event>.Update
            .Set(x => x.ImageId, newId);

        var filter = Builders<Event>.Filter.Where(x => x.ImageId == imageId);
        var result = await _events.UpdateManyAsync(filter, update);

        return result.IsAcknowledged;
    }

    public async Task<bool> DeleteEventsInSpace(Guid spaceId)
    {
        var filter = Builders<Event>.Filter.Where(x => x.SpaceId == spaceId);
        var result = await _events.DeleteOneAsync(filter);

        return result.IsAcknowledged;
    }
}