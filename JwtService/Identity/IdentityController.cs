using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JwtService.Identity;

[ApiController]
[Route("[controller]")]
public class IdentityController : ControllerBase
{
    private readonly AuthOptions _options;

    public IdentityController(IOptions<AuthOptions> options)
    {
        _options = options.Value;
    }

    [HttpPost("token")]
    [ProducesResponseType(statusCode: 200, type: typeof(GetTokenResponse))]
    public IActionResult Get([FromBody] GetTokenCommand command)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, command.Username)
        };

        var token = new JwtSecurityToken(
            issuer: _options.Authority,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)),
            signingCredentials: new SigningCredentials(
                key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecurityKey)),
                algorithm: SecurityAlgorithms.HmacSha256));

        return Ok(new GetTokenResponse()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }

}