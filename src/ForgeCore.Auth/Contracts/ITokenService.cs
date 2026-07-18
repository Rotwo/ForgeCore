using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ForgeCore.Auth.Contracts
{
    public interface ITokenService
    {
        string GenerateAccessToken(Guid accountId, Guid sessionId);
        string GenerateRefreshToken();
        ClaimsPrincipal? ValidateAccessToken(string token);
    }
}
