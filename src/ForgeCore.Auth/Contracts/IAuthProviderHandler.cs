using ForgeCore.Auth.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeCore.Auth.Contracts
{
    public interface IAuthProviderHandler
    {
        AuthProviderType ProviderType { get; }
        Task<ExternalAuthResult> ValidateAsync(string credentials);
    }

    public class ExternalAuthResult
    {
        public string ProviderUserId { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? DisplayName { get; set; }
    }
}
