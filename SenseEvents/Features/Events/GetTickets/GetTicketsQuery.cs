using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SenseEvents.Features.Events.GetTickets;

public class GetTicketsQuery : IRequest<GetTicketsResponse>
{
    [FromRoute(Name = "id")]
    public Guid EventId { get; init; }
}