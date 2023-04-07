﻿namespace SenseEvents.Features.Events.UpdateEvent
{
    public class UpdateEventRequest
    {
        public DateTime StartUtc { get; set; }

        public DateTime EndUtc { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid ImageId { get; set; }

        public Guid SpaceId { get; set; }
    }
}