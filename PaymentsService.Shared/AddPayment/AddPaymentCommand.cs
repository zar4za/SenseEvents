using JetBrains.Annotations;

namespace PaymentsService.Shared.AddPayment;

public class AddPaymentCommand
{
    [UsedImplicitly]
    public string Description { get; set; } = null!;

    [UsedImplicitly] 
    public decimal Amount { get; set; }
}