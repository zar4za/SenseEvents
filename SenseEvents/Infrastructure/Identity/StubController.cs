using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SenseEvents.Infrastructure.Identity
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class StubController : ControllerBase
    {

        [HttpGet("authstub")]
        public async Task<IActionResult> Ping()
        {
            return Ok(new PingResponse()
            {
                Message = "pong"
            });
        }
    }
}
