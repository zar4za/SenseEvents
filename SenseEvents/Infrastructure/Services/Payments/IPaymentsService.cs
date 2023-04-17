using PaymentsService;
using PaymentsService.AddPayment;

namespace SenseEvents.Infrastructure.Services.Payments
{
    public interface IPaymentsService
    {
        Payment Create(AddPaymentCommand command);

        Payment Confirm(Guid id);

        Payment Cancel (Guid id);
    }
}
