using Microsoft.Extensions.Options;
using PaymentsService.Shared;
using PaymentsService.Shared.AddPayment;
using System.Text.Json;

namespace SenseEvents.Infrastructure.Services.Payments;

public class PaymentsHttpService : IPaymentsService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly ServiceOptions _options;
    private readonly JsonSerializerOptions _serializerOptions;

    public PaymentsHttpService(IHttpClientFactory factory, IOptions<ServiceOptions> options)
    {
        _clientFactory = factory;
        _options = options.Value;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<Payment> Create(AddPaymentCommand command)
    {
        using var client = _clientFactory.CreateClient(ServiceOptions.HttpClientName);
        var uri = _options.PaymentsServiceUrl;
        var response = await client.PostAsync(uri, JsonContent.Create(command));
        var payment = JsonSerializer.Deserialize<Payment>(await response.Content.ReadAsStreamAsync(), _serializerOptions);
        return payment!;
    }

    public async Task<Payment> Confirm(Guid id)
    {
        using var client = _clientFactory.CreateClient(ServiceOptions.HttpClientName);
        var uri = $"{_options.PaymentsServiceUrl}/{id}/confirm";
        var response = await client.PutAsync(uri, null);
        var payment = JsonSerializer.Deserialize<Payment>(await response.Content.ReadAsStreamAsync(), _serializerOptions);
        return payment!;
    }

    public async Task<Payment> Cancel(Guid id)
    {
        using var client = _clientFactory.CreateClient(ServiceOptions.HttpClientName);
        var uri = _options.PaymentsServiceUrl + $"/{id}/cancel";
        var response = await client.PutAsync(uri, null);
        var payment = JsonSerializer.Deserialize<Payment>(await response.Content.ReadAsStreamAsync(), _serializerOptions);
        return payment!;
    }
}