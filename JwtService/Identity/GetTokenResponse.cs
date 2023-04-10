using JetBrains.Annotations;

namespace JwtService.Identity;

public class GetTokenResponse
{
    [UsedImplicitly] // json serialization
    public string Token { get; set; } = null!;
}