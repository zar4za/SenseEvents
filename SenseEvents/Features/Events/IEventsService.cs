﻿using SenseEvents.Features.Events.AddEvent;

namespace SenseEvents.Features.Events
{
    public interface IEventsService
    {
        Task<Guid> AddEvent(AddEventCommand command);

        Task<IEnumerable<Event>> GetEvents();

        Task<Event?> GetEvent(Guid id);
    }
}
