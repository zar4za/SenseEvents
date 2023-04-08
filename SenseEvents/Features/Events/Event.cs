﻿using SenseEvents.Features.Tickets;

namespace SenseEvents.Features.Events
{
    public class Event
    {
        private readonly List<Ticket> _tickets = null!;

        public Guid Id { get; init; }

        public DateTime StartUtc { get; set; }

        public DateTime EndUtc { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public Guid ImageId { get; set; }

        public Guid SpaceId { get; set; }

        public IEnumerable<Ticket> Tickets
        {
            get => _tickets;
            init => _tickets = value.ToList();
        }

        public void AddTicket(Ticket ticket)
        {
            _tickets.Add(ticket);
        }
    }
}
