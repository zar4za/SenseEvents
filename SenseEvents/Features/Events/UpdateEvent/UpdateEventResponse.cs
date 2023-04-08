namespace SenseEvents.Features.Events.UpdateEvent
{
    /// <summary>
    /// Модель ответа на обновление информации о мероприятии.
    /// </summary>
    public class UpdateEventResponse
    {
        /// <summary>
        /// Флаг, показывающий успешность обновления мероприятия.
        /// </summary>
        public bool Success { get; set; }
    }
}
