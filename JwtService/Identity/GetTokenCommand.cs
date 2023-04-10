using JetBrains.Annotations;

namespace JwtService.Identity;

public class GetTokenCommand
{
    [UsedImplicitly] // Json parsing
    public string Username { get; init; } = null!;

    [UsedImplicitly] // mock credentials
    public string Password { get; init; } = null!;
}