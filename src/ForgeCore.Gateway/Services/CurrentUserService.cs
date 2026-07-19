using System.IdentityModel.Tokens.Jwt;
using ForgeCore.Shared.Contracts;

namespace ForgeCore.Gateway.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public Guid AccountId { get; }
        public Guid SessionId { get; }

        public CurrentUserService(IHttpContextAccessor accessor)
        {
            var user = accessor.HttpContext?.User;
            AccountId = Guid.TryParse(user?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value, out var id) ? id : Guid.Empty;
            SessionId = Guid.TryParse(user?.FindFirst("sid")?.Value, out var sid) ? sid : Guid.Empty;
        }
    }
}