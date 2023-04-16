namespace PaymentsService.AddPayment
{
    public class AddPaymentCommand
    {
        public string Description { get; set; }

        public decimal Amount { get; set; }
    }
}
