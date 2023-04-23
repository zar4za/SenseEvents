using Microsoft.Extensions.Options;

namespace SenseEvents.Infrastructure.Services.Images;

public class ImageHttpService : IImageService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly ServiceOptions _options;

    public ImageHttpService(IHttpClientFactory factory, IOptions<ServiceOptions> options)
    {
        _clientFactory = factory;
        _options = options.Value;
    }

    public bool ImageExists(Guid id)
    {
        using var client = _clientFactory.CreateClient(ServiceOptions.HttpClientName);
        var uri = $"{_options.ImageServiceUrl}/{id}";
        var response = client.GetFromJsonAsync<ImageResponse>(uri).Result; 
        return response?.Exists == true;
    }
}