namespace SenseEvents.Infrastructure.Middleware
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
