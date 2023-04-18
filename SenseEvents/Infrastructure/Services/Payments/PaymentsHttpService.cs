using Microsoft.Extensions.Options;
using System.Text.Json;
using PaymentsService.Shared;
using PaymentsService.Shared.AddPayment;

namespace SenseEvents.Infrastructure.Services.Payments;

public class PaymentsHttpService : IPaymentsService
{
    private readonly HttpClient _httpClient;
    private readonly ServiceOptions _options;

    public PaymentsHttpService(HttpClient client, IOptions<ServiceOptions> options)
    {
        _httpClient = client;
        _options = options.Value;
    }

    public async Task<Payment> Create(AddPaymentCommand command)
    {
        var uri = _options.PaymentsServiceUrl + "/api/payments";
        var response = await _httpClient.PostAsync(uri, JsonContent.Create(command));
        var payment = JsonSerializer.Deserialize<Payment>(await response.Content.ReadAsStreamAsync());
        return payment!;
    }

    public async Task<Payment> Confirm(Guid id)
    {
        var uri = _options.PaymentsServiceUrl + $"/api/payments/{id}/confirm";
        var response = await _httpClient.PutAsync(uri, null);
        var payment = JsonSerializer.Deserialize<Payment>(await response.Content.ReadAsStreamAsync());
        return payment!;
    }

    public async Task<Payment> Cancel(Guid id)
    {
        var uri = _options.PaymentsServiceUrl + $"/api/payments/{id}/cancel";
        var response = await _httpClient.PutAsync(uri, null);
        var payment = JsonSerializer.Deserialize<Payment>(await response.Content.ReadAsStreamAsync());
        return payment!;
    }
}