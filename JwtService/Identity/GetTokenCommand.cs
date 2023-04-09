using JetBrains.Annotations;

namespace JwtService.Identity
{
    public class GetTokenCommand
    {
        [UsedImplicitly]
        public string Username { get; init; } = null!;

        public string Password { get; init; } = null!;
    }
}
