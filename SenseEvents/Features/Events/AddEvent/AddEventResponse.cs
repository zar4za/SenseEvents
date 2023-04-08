namespace SenseEvents.Features.Events.AddEvent
{
    /// <summary>
    /// Модель ответа на добавление мероприятия.
    /// </summary>
    public class AddEventResponse
    {
        /// <summary>
        /// Идентификатор, присвоенный добавленному мероприятию.
        /// </summary>
        public Guid Id { get; set; }
    }
}
