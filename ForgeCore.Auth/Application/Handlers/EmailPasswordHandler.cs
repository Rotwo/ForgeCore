using ForgeCore.Auth.Contracts;
using ForgeCore.Auth.Domain;
using System.Text.Json;

namespace ForgeCore.Auth.Application.Handlers
{
    public class EmailPasswordHandler : IAuthProviderHandler
    {
        public AuthProviderType ProviderType => AuthProviderType.EmailPassword;

        private readonly IAuthProviderRepository _authProviderRepository;

        public EmailPasswordHandler(IAuthProviderRepository authProviderRepository)
        {
            _authProviderRepository = authProviderRepository;
        }

        public async Task<ExternalAuthResult> ValidateAsync(string credentials)
        {
            var request = JsonSerializer.Deserialize<EmailPasswordCredentialsPayload>(credentials);

            if (request is null)
                throw new ArgumentException("Invalid credentials format.");

            var provider = await _authProviderRepository.GetByProviderAsync(
                AuthProviderType.EmailPassword,
                request.Email
            );

            if (provider is null || string.IsNullOrEmpty(provider.CredentialsHash))
                throw new UnauthorizedAccessException("Invalid email or password.");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, provider.CredentialsHash))
                throw new UnauthorizedAccessException("Invalid email or password.");

            return new ExternalAuthResult
            {
                ProviderUserId = provider.ProviderUserId,
                Email = provider.Email,
            };
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        public class EmailPasswordCredentialsPayload
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
    }
}
