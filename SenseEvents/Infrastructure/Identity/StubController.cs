using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;

namespace SenseEvents.Infrastructure.Identity;

[Route("[controller]")]
[ApiController]
[Authorize]
public class StubController : ControllerBase
{
    /// <summary>
    /// Метод - заглушка. Нужен только для проверки аутентификации через Jwt.
    /// </summary>
    /// <returns></returns>
    [HttpGet("authstub")]
    [ProducesResponseType(statusCode: 200, type: typeof(PingResponse))]
    [ProducesResponseType(statusCode: 400, type: typeof(ScError))]
    [ProducesResponseType(statusCode: 401)]
    [ProducesResponseType(statusCode: 500, type: typeof(ScError))]
    public IActionResult Ping()
    {
        return Ok(new PingResponse
        {
            Message = "pong"
        });
    }
}