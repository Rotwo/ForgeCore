using ForgeCore.Auth.Application.Handlers;
using ForgeCore.Auth.Contracts;
using ForgeCore.Auth.Domain;
using ForgeCore.Players.Contracts;
using ForgeCore.Shared.Contracts;
using ForgeCore.Shared.DTOs;

namespace ForgeCore.Auth.Application
{
    public class AuthService : IAuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IAuthProviderRepository _authProviderRepository;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEnumerable<IAuthProviderHandler> _handlers;

        public AuthService(IAccountRepository accountRepository, IPlayerRepository playerRepository, ISessionRepository sessionRepository, ITokenService tokenService, IAuthProviderRepository authProviderRepository, IEnumerable<IAuthProviderHandler> handlers, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _authProviderRepository = authProviderRepository ?? throw new ArgumentNullException(nameof(authProviderRepository));
            _handlers = handlers ?? throw new ArgumentNullException(nameof(handlers));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<AuthResultDto> LoginGuestAsync()
        {
            // 1. Create Account (anonymous)
            var account = new Account(
                email: null,
                displayName: $"Guest_{Guid.NewGuid():N}"[..14]
            );

            _accountRepository.Add(account);

            // 2. Create Session
            var refreshToken = _tokenService.GenerateRefreshToken();

            var session = new Session(
                accountId: account.Id,
                refreshToken: refreshToken,
                duration: TimeSpan.FromDays(7)
            );

            _sessionRepository.Add(session);

            // 3. Access token
            var accessToken = _tokenService.GenerateAccessToken(
                account.Id,
                session.Id
            );

            await _unitOfWork.SaveChangesAsync();

            return new AuthResultDto
            {
                AccountId = account.Id,
                SessionId = session.Id,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };
        }

        public async Task LogoutAsync(Guid sessionId)
        {
            await _sessionRepository.RevokeAsync(sessionId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<AuthResultDto> RefreshAsync(string refreshToken)
        {
            var session = await _sessionRepository.GetByRefreshTokenAsync(refreshToken);

            if (session == null || session.IsExpired())
                throw new UnauthorizedAccessException("Invalid session");

            var account = await _accountRepository.GetByIdAsync(session.AccountId);

            if (account == null)
                throw new Exception("Account not found");

            var accessToken = _tokenService.GenerateAccessToken(
                account.Id,
                session.Id
            );

            return new AuthResultDto
            {
                AccountId = account.Id,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthResultDto> RegisterEmailAsync(string email, string password, string displayName)
        {
            var normalizedEmail = email.Trim().ToLowerInvariant();

            if (await _authProviderRepository.ExistsAsync(AuthProviderType.EmailPassword, normalizedEmail))
                throw new InvalidOperationException("An account with this email already exists");

            var passwordHash = EmailPasswordHandler.HashPassword(password);

            var account = new Account(normalizedEmail, displayName);
            _accountRepository.Add(account);

            var provider = new AuthProvider(
                type: AuthProviderType.EmailPassword,
                providerUserId: normalizedEmail,
                email: normalizedEmail,
                credentialsHash: passwordHash);

            account.LinkProvider(provider);
            _authProviderRepository.Add(provider);

            var refreshToken = _tokenService.GenerateRefreshToken();
            var session = new Session(account.Id, refreshToken, TimeSpan.FromDays(7));
            _sessionRepository.Add(session);

            var accessToken = _tokenService.GenerateAccessToken(account.Id, session.Id);

            await _unitOfWork.SaveChangesAsync();

            return new AuthResultDto
            {
                AccountId = account.Id,
                SessionId = session.Id,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };
        }

        public Task LinkProviderAsync(Guid accountId, AuthProviderType providerType, string credentials)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResultDto> LoginAsync(AuthProviderType providerType, string credentials)
        {
            var handler = _handlers.FirstOrDefault(h => h.ProviderType == providerType)
                ?? throw new ArgumentException($"Unsupported provider type: {providerType}");

            var result = await handler.ValidateAsync(credentials);

            var existingProvider = await _authProviderRepository.GetByProviderAsync(
                providerType,
                result.ProviderUserId
            );

            Account account;

            if (existingProvider != null)
            {
                account = await _accountRepository.GetByIdAsync(existingProvider.AccountId)
                    ?? throw new Exception("Account not found for linked provider");
            }
            else
            {
                account = new Account(result.Email, result.DisplayName ?? "Player");
                _accountRepository.Add(account);

                var provider = new AuthProvider(
                    type: providerType,
                    providerUserId: result.ProviderUserId,
                    email: result.Email
                );

                account.LinkProvider(provider);
                _authProviderRepository.Add(provider);
            }

            var refreshToken = _tokenService.GenerateRefreshToken();
            var session = new Session(account.Id, refreshToken, TimeSpan.FromDays(7));
            _sessionRepository.Add(session);

            var accessToken = _tokenService.GenerateAccessToken(account.Id, session.Id);

            await _unitOfWork.SaveChangesAsync();

            return new AuthResultDto
            {
                AccountId = account.Id,
                SessionId = session.Id,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };
        }
    }
}
