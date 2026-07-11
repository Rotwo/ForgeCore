using ForgeCore.Auth.Contracts;
using ForgeCore.Auth.Domain;
using ForgeCore.Players.Contracts;
using ForgeCore.Players.Domain;
using ForgeCore.Shared.DTOs;
using ForgeCore.Shared.Contracts;

namespace ForgeCore.Auth.Application
{
    public class AuthService : IAuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IAccountRepository accountRepository, IPlayerRepository playerRepository, ISessionRepository sessionRepository, ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
            _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<AuthResultDto> LoginGuestAsync()
        {
            // 1. Create Account (anonymous)
            var account = new Account(
                email: null,
                displayName: "Guest"
            );

            _accountRepository.Add(account);

            // 2. Create Player
            var player = new Player(
                accountId: account.Id,
                nickname: account.DisplayName
            );

            _playerRepository.Add(player);

            // 3. Create Session
            var refreshToken = _tokenService.GenerateRefreshToken();

            var session = new Session(
                accountId: account.Id,
                refreshToken: refreshToken,
                duration: TimeSpan.FromDays(7)
            );

            _sessionRepository.Add(session);

            // 4. Access token
            var accessToken = _tokenService.GenerateAccessToken(
                account.Id,
                session.Id
            );

            await _unitOfWork.SaveChangesAsync();

            return new AuthResultDto
            {
                AccountId = account.Id,
                PlayerId = player.Id,
                SessionId = session.Id,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };
        }

        public async Task LogoutAsync(Guid sessionId)
        {
            await _sessionRepository.RevokeAsync(sessionId);
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
                PlayerId = Guid.Empty, // not needed right now, but can be fetched if needed
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
