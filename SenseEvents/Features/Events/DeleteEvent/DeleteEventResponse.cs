namespace SenseEvents.Features.Events.DeleteEvent
{
    /// <summary>
    /// Модель ответа на удаление мероприятия.
    /// </summary>
    public class DeleteEventResponse
    {
        /// <summary>
        /// Флаг, показывающий успешность удаления.
        /// </summary>
        public bool Success { get; set; }
    }
}
