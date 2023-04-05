using SenseEvents.Features.Events.AddEvent;
using SenseEvents.Features.Id;

namespace SenseEvents.Features.Events
{
    public class EventsServiceMock : IEventsService
    {
        private readonly List<Event> _events = new();
        private readonly IGuidService _guidService;

        public EventsServiceMock(IGuidService guidService)
        {
            _guidService = guidService;
        }


        public async Task<Guid> AddEvent(AddEventCommand command)
        {
            var id = _guidService.GetNewId();

            await Task.Run(() =>
            { 
                _events.Add(new Event()
                {
                    Id = id,
                    Name = command.Name,
                    Description = command.Description,
                    StartUtc = command.StartUtc,
                    EndUtc = command.EndUtc,
                    ImageId = command.ImageId,
                    SpaceId = command.SpaceId
                });
            });

            return id;
        }
    }
}
