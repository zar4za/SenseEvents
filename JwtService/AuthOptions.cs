using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtService
{
    public class AuthOptions
    {
        public const string ConfigSection = "Auth";

        public string Audience { get; init; } = null!;

        public string Authority { get; init; } = null!;

        public string SecurityKey { get; init; } = null!;

        public SecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
        }
    }
}
