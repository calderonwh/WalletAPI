using Microsoft.AspNetCore.Mvc;
using WalletAPI.Application.Interfaces;
using WalletAPI.Application.Services;
using WalletAPI.Domain.Entities;

namespace WalletAPI.Api.Controllers
{
    [Route("api/wallets")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly ILogger<WalletService> _logger;

        public WalletsController(IWalletService walletService, ILogger<WalletService> logger)
        {
            _walletService = walletService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var wallets = await _walletService.GetAllWalletsAsync();
                return Ok(wallets);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var wallet = await _walletService.GetWalletByIdAsync(id);
                return Ok(wallet);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Wallet wallet)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdWallet = await _walletService.CreateWalletAsync(wallet);
                return CreatedAtAction(nameof(GetById), new { id = createdWallet.Id }, createdWallet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", error = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Wallet wallet)
        {
            if (id != wallet.Id)
                return BadRequest(new { message = "El ID de la URL no coincide con el del cuerpo de la solicitud." });

            try
            {
                var updated = await _walletService.UpdateWalletAsync(wallet);
                return updated ? NoContent() : NotFound(new { message = "Billetera no encontrada." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el endpoint PUT /api/wallets/{id}");
                return StatusCode(500, new { message = "Ocurrió un error inesperado." });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _walletService.DeleteWalletAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
        {
            try
            {
                var result = await _walletService.TransferBalanceAsync(request.FromWalletId, request.ToWalletId, request.Amount);
                return result ? Ok(new { message = "Transferencia exitosa" }) : BadRequest(new { message = "Transferencia fallida" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", error = ex.Message });
            }
        }

    }

    public class TransferRequest
    {
        public int FromWalletId { get; set; }
        public int ToWalletId { get; set; }
        public decimal Amount { get; set; }
    }
}
