namespace SenseEvents.Infrastructure.Services
{
    public class ServiceOptions
    {
        public const string ConfigSection = "Services";

        public string ImageServiceUrl { get; set; } = null!;

        public string SpaceServiceUrl { get; set; } = null!;

        public string PaymentsServiceUrl { get; set; } = null!;

        public string ApiToken { get; set; } = null!;
    }
}
