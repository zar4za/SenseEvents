using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SenseEvents.Infrastructure.Identity;

[Route("[controller]")]
[ApiController]
[Authorize]
public class StubController : ControllerBase
{

    [HttpGet("authstub")]
    public IActionResult Ping()
    {
        return Ok(new PingResponse
        {
            Message = "pong"
        });
    }
}