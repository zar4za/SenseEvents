using JetBrains.Annotations;
using MediatR;
using PaymentsService.Shared.AddPayment;
using SC.Internship.Common.Exceptions;
using SenseEvents.Features.Events;
using SenseEvents.Infrastructure.Identity;
using SenseEvents.Infrastructure.Services.Payments;

namespace SenseEvents.Features.Tickets.AddTicket;

[UsedImplicitly] //mediator
public class AddTicketHandler : IRequestHandler<AddTicketCommand, AddTicketResponse>
{
    private readonly IGuidService _guidService;
    private readonly IEventsService _eventsService;
    private readonly IPaymentsService _paymentsService;

    public AddTicketHandler(IGuidService guidService, IEventsService eventsService, IPaymentsService paymentsService)
    {
        _guidService = guidService;
        _eventsService = eventsService;
        _paymentsService = paymentsService;
    }

    public async Task<AddTicketResponse> Handle(AddTicketCommand request, CancellationToken cancellationToken)
    {
        var plannedEvent = await _eventsService.GetEvent(request.EventId);
        var payment = await _paymentsService.Create(new AddPaymentCommand
        {
            Amount = plannedEvent.TicketPrice,
            Description = $"Ticket for {plannedEvent.Name}"
        });

        try
        {
            var ticket = await _eventsService.AddTicket(plannedEvent.Id, new Ticket
            {
                Id = _guidService.GetNewId(),
                OwnerId = request.OwnerId
            });

            await _paymentsService.Confirm(payment.Id);

            return new AddTicketResponse
            {
                Ticket = ticket
            };
        }
        catch
        {
            await _paymentsService.Cancel(payment.Id);
            throw new ScException("Failed to buy ticket");
        }
    }
}