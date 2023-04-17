using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace SenseEvents.Infrastructure.Services.Spaces
{
    public class SpaceHttpService : ISpaceService
    {
        private readonly HttpClient _httpClient;
        private readonly ServiceOptions _options;

        public SpaceHttpService(HttpClient client, IOptions<ServiceOptions> options)
        {
            _httpClient = client;
            _options = options.Value;
        }

        public bool SpaceExists(Guid id)
        {
            var uri = _options.SpaceServiceUrl + $"/{id}";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_options.ApiToken);
            var response = _httpClient.GetFromJsonAsync<SpaceResponse>(uri).Result; 
            return response?.Exists == true;
        }
    }
}
