﻿namespace PaymentsService
{
    public class Payment
    {
        public Guid Id { get; set; }

        public PaymentState State { get; set; }
        
        public DateTimeOffset DateCreation { get; set; }

        public DateTimeOffset? DateConfirmation { get; set; }

        public DateTimeOffset? DateCancellation { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }
    }
}