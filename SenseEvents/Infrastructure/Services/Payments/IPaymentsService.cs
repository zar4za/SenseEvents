using PaymentsService.Shared;
using PaymentsService.Shared.AddPayment;

namespace SenseEvents.Infrastructure.Services.Payments;

public interface IPaymentsService
{
    Task<Payment> Create(AddPaymentCommand command);

    Task<Payment> Confirm(Guid id);

    Task<Payment> Cancel (Guid id);
}