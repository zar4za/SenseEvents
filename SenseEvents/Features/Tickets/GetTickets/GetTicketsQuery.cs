﻿using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SenseEvents.Features.Tickets.GetTickets;

public class GetTicketsQuery : IRequest<GetTicketsResponse>
{
    [FromRoute(Name = "id")]
    public Guid EventId { get; [UsedImplicitly] init; } // route binding
}