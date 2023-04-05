using Microsoft.AspNetCore.Mvc;

namespace SenseEvents.Features.Events
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public void Put(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
