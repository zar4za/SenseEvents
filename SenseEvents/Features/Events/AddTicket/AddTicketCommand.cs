using System.Text.Json.Serialization;
using MediatR;

namespace SenseEvents.Features.Events.AddTicket
{
    public class AddTicketCommand : IRequest<AddTicketResponse>
    {
        [JsonIgnore]
        public Guid EventId { get; set; }

        public Guid OwnerId { get; set; }

        public int? Seat { get; set; }
    }
}
