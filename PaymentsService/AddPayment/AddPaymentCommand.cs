using JetBrains.Annotations;

namespace PaymentsService.AddPayment;

public class AddPaymentCommand
{
    [UsedImplicitly]
    public string Description { get; set; } = null!;

    [UsedImplicitly] 
    public decimal Amount { get; set; }
}