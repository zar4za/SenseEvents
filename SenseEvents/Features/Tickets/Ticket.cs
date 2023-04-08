namespace SenseEvents.Features.Tickets
{
    /// <summary>
    /// Модель билета на мероприятие.
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// Идентификатор билета.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор владельца билета.
        /// </summary>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Номер места в пространстве. Может не указываться.
        /// </summary>
        public int? Seat { get; set; }
    }
}
