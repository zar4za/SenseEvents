using System.Text.Json.Serialization;
using MediatR;

namespace SenseEvents.Features.Events.AddTicket
{
    /// <summary>
    /// Модель запроса для выдачи билета на мероприятие.
    /// </summary>
    public class AddTicketCommand : IRequest<AddTicketResponse>
    {
        /// <summary>
        /// Уникальный идентификатор мероприятия.
        /// </summary>

        [JsonIgnore]
        public Guid EventId { get; set; }

        /// <summary>
        /// Идентификатор владельца билета.
        /// </summary>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Номер места в пространстве.
        /// </summary>
        public int? Seat { get; set; }
    }
}
