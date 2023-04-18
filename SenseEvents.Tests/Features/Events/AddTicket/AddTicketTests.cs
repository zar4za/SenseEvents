using SenseEvents.Features.Events;
using SenseEvents.Features.Events.AddTicket;
using SenseEvents.Infrastructure.Identity;

namespace SenseEvents.Tests.Features.Events.AddTicket;

[TestFixture]
internal class AddTicketTests
{
    private Guid _ticketGuid;
    private IGuidService _guidService = null!;

    [SetUp]
    public void SetUp()
    {
        _ticketGuid = Guid.NewGuid();
        var guidService = new Mock<IGuidService>();
        guidService
            .Setup(x => x.GetNewId())
            .Returns(_ticketGuid);

        _guidService = guidService.Object;
    }


    [Test]
    public async Task Handle_EventExists_ShouldAddTicket()
    {
        var eventId = Guid.NewGuid();
        var ownerId = Guid.NewGuid();
        var command = new AddTicketCommand
        {
            EventId = eventId,
            OwnerId = ownerId
        };

        var ticket = new Ticket
        {
            Id = _ticketGuid,
            OwnerId = ownerId,
            Seat = 1
        };

        var eventsService = new Mock<IEventsService>();
        eventsService
            .Setup(x => x.AddTicket(eventId, ticket))
            .ReturnsAsync(ticket);

        var handler = new AddTicketHandler(_guidService, eventsService.Object, null!);


        var result = await handler.Handle(command, CancellationToken.None);


        Assert.That(result, Is.Not.Null);
        Assert.That(result.Ticket.Id, Is.EqualTo(ticket.Id));
    }
}