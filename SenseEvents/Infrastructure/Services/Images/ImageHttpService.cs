using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace SenseEvents.Infrastructure.Services.Images;

public class ImageHttpService : IImageService
{
    private readonly HttpClient _httpClient;
    private readonly ServiceOptions _options;
    private readonly ILogger<ImageHttpService> _logger;

    public ImageHttpService(ILogger<ImageHttpService> logger, HttpClient client, IOptions<ServiceOptions> options)
    {
        _logger = logger;
        _httpClient = client;
        _options = options.Value;
    }

    public bool ImageExists(Guid id)
    {
        var uri = _options.ImageServiceUrl + $"/{id}";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_options.ApiToken);

        _logger.LogInformation($"Sending request to {uri}");
        var response = _httpClient.GetFromJsonAsync<ImageResponse>(uri).Result; 
        return response?.Exists == true;
    }
}