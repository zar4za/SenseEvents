using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace SenseEvents.Infrastructure.Services.Images;

public class ImageHttpService : IImageService
{
    private readonly HttpClient _httpClient;
    private readonly ServiceOptions _options;

    public ImageHttpService(HttpClient client, IOptions<ServiceOptions> options)
    {
        _httpClient = client;
        _options = options.Value;
    }

    public bool ImageExists(Guid id)
    {
        var uri = _options.ImageServiceUrl + $"/{id}";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_options.ApiToken);
        var response = _httpClient.GetFromJsonAsync<ImageResponse>(uri).Result; 
        return response?.Exists == true;
    }
}