using Microsoft.Extensions.Options;

namespace SenseEvents.Infrastructure.Services.Spaces;

public class SpaceHttpService : ISpaceService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly ServiceOptions _options;

    public SpaceHttpService(IHttpClientFactory factory, IOptions<ServiceOptions> options)
    {
        _clientFactory = factory;
        _options = options.Value;
    }

    public bool SpaceExists(Guid id)
    {
        using var client = _clientFactory.CreateClient(ServiceOptions.HttpClientName);
        var uri = $"{_options.SpaceServiceUrl}/{id}";
        var response = client.GetFromJsonAsync<SpaceResponse>(uri).Result; 
        return response?.Exists == true;
    }
}