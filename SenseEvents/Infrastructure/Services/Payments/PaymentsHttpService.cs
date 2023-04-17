using Microsoft.Extensions.Options;
using PaymentsService;
using PaymentsService.AddPayment;
using System.Text.Json;

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

    public Payment Create(AddPaymentCommand command)
    {
        var uri = _options.PaymentsServiceUrl + "/api/payments";
        var response = _httpClient.PostAsync(uri, JsonContent.Create(command)).Result;
        var payment = JsonSerializer.Deserialize<Payment>(response.Content.ReadAsStream());
        return payment!;
    }

    public Payment Confirm(Guid id)
    {
        var uri = _options.PaymentsServiceUrl + $"/api/payments/{id}/confirm";
        var response = _httpClient.PutAsync(uri, null).Result;
        var payment = JsonSerializer.Deserialize<Payment>(response.Content.ReadAsStream());
        return payment!;
    }

    public Payment Cancel(Guid id)
    {
        var uri = _options.PaymentsServiceUrl + $"/api/payments/{id}/cancel";
        var response = _httpClient.PutAsync(uri, null).Result;
        var payment = JsonSerializer.Deserialize<Payment>(response.Content.ReadAsStream());
        return payment!;
    }
}