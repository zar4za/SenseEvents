namespace JwtService.Identity
{
    public class GetTokenCommand
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
