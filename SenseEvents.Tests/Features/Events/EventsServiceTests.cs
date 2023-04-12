using Microsoft.Extensions.Options;
using Mongo2Go;
using SenseEvents.Features.Events;
using SenseEvents.Infrastructure.Identity;

namespace SenseEvents.Tests.Features.Events;

[TestFixture]
internal class EventsServiceTests : IDisposable
{
    private Guid _eventId;
    private MongoDbRunner _runner = null!;
    private IEventsService _eventsService = null!;

    [SetUp]
    public void SetUp()
    {
        _runner = MongoDbRunner.StartForDebugging();
        var options = new EventsMongoOptions
        {
            ConnectionString = _runner.ConnectionString,
            EventsCollectionName = "events",
            DatabaseName = "sense-test"
        };
        var guidService = new Mock<IGuidService>();
        guidService.Setup(x => x.GetNewId())
            .Returns(Guid.NewGuid());


        _eventsService = new EventsService(Options.Create(options), guidService.Object);
    }

    [Test]
    public async Task AddEvent_ValidEvent_ReturnsAddedId()
    {
        var id = await _eventsService.AddEvent(new Event
        {
            Description = "test",
            EndUtc = DateTime.UtcNow.AddDays(2),
            Id = Guid.NewGuid(),
            ImageId = Guid.NewGuid(),
            MaxTickets = 0,
            Name = "test",
            SpaceId = Guid.NewGuid(),
            StartUtc = DateTime.UtcNow,
            Tickets = new List<Ticket>()
        });

        Assert.That(id, Is.Not.Empty);
        _eventId = id;
    }

    [Test]
    public void AddTicket_ReachedLimit_ThrowsInvalidOperationException()
    {
        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await _eventsService.AddTicket(_eventId, new Ticket
            {
                Id = Guid.NewGuid(),
                OwnerId = Guid.NewGuid(),
                Seat = 1
            });
        });
    }

    [Test]
    public async Task RemoveTicket_CorrectId_ReturnsTrue()
    {
        var success = await _eventsService.DeleteEvent(_eventId);
        Assert.That(success, Is.True);
    }


    public void Dispose()
    {
        _runner.Dispose();
    }
}