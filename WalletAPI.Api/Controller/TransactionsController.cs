using Microsoft.AspNetCore.Mvc;
using WalletAPI.Application.Interfaces;
using WalletAPI.Domain.Entities;

namespace WalletAPI.Api.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("wallet/{walletId}")]
        public async Task<IActionResult> GetByWalletId(int walletId)
        {
            var transactions = await _transactionService.GetTransactionsByWalletIdAsync(walletId);
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null) return NotFound();
            return Ok(transaction);
        }
    }
}
