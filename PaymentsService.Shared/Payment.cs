using JetBrains.Annotations;

namespace PaymentsService.Shared;

public class Payment
{
    public Guid Id { get; set; }

    public PaymentState State { get; set; }
        
    public DateTimeOffset DateCreation { get; set; }

    public DateTimeOffset? DateConfirmation { get; set; }

    public DateTimeOffset? DateCancellation { get; set; }

    [UsedImplicitly]
    public decimal Amount { get; set; }

    [UsedImplicitly]
    public string Description { get; set; } = null!;
}