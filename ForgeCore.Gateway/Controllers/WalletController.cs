using ForgeCore.Economy.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ForgeCore.Economy.Contracts.Requests;

namespace ForgeCore.Gateway.Controllers
{
    [Route("api/econommy/wallet")]
    [ApiController]
    [Authorize]
    public partial class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletRequest request)
        {
            if (request is null || request.OwnerId == Guid.Empty)
                return BadRequest("ownerId is required");

            try
            {
                var wallet = await _walletService.CreateWalletAsync(request.OwnerId);
                return CreatedAtAction(nameof(GetBalances), new { walletId = wallet.Id }, wallet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> GetWalletsByOwner(Guid ownerId)
        {
            if (ownerId == Guid.Empty)
                return BadRequest("ownerId is required");

            var wallets = await _walletService.GetWalletsByOwnerIdAsync(ownerId);
            return Ok(wallets);
        }

        [HttpGet("{walletId}/balances")]
        public async Task<IActionResult> GetBalances(Guid walletId)
        {
            if (walletId == Guid.Empty)
                return BadRequest("walletId is required");

            try
            {
                var balances = await _walletService.GetBalancesAsync(walletId);
                return Ok(balances);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{walletId}/balances/{currencyId}")]
        public async Task<IActionResult> GetBalanceAmount(Guid walletId, Guid currencyId)
        {
            if (walletId == Guid.Empty || currencyId == Guid.Empty)
                return BadRequest("walletId and currencyId are required");

            try
            {
                var balance = await _walletService.GetBalanceAmountAsync(walletId, currencyId);
                return Ok(balance);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{walletId}/deposit")]
        public async Task<IActionResult> Deposit(Guid walletId, [FromBody] WalletMovementRequest request)
        {
            if (walletId == Guid.Empty)
                return BadRequest("walletId is required");
            if (request is null || request.Amount <= 0 || request.CurrencyId == Guid.Empty)
                return BadRequest("amount must be greater than zero and currencyId is required");

            try
            {
                await _walletService.DepositAsync(walletId, request.Amount, request.CurrencyId);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{walletId}/withdraw")]
        public async Task<IActionResult> Withdraw(Guid walletId, [FromBody] WalletMovementRequest request)
        {
            if (walletId == Guid.Empty)
                return BadRequest("walletId is required");
            if (request is null || request.Amount <= 0 || request.CurrencyId == Guid.Empty)
                return BadRequest("amount must be greater than zero and currencyId is required");

            try
            {
                await _walletService.WithdrawAsync(walletId, request.Amount, request.CurrencyId);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
