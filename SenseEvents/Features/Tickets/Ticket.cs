namespace SenseEvents.Features.Tickets
{
    public class Ticket
    {
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }

        public int? Seat { get; set; }
    }
}
