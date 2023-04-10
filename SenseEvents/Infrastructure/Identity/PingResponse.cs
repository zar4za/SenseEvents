using JetBrains.Annotations;

namespace SenseEvents.Infrastructure.Identity
{
    public class PingResponse
    {
        [UsedImplicitly] // json serialization
        public string Message { get; set; } = null!;
    }
}
