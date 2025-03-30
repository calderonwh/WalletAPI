using WalletAPI.Application.Interfaces;
using WalletAPI.Domain.Entities;
using WalletAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.Extensions.Logging;

namespace WalletAPI.Application.Services
{
    public class WalletService : IWalletService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WalletService> _logger;

        public WalletService(ApplicationDbContext context, ILogger<WalletService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Wallet>> GetAllWalletsAsync()
        {
            var wallets = await _context.Wallets.ToListAsync();

            if (wallets == null || wallets.Count == 0)
                throw new KeyNotFoundException("No hay billeteras registradas.");

            return wallets;
        }


        public async Task<Wallet> GetWalletByIdAsync(int id)
        {
            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet == null)
                throw new KeyNotFoundException($"La billetera con ID {id} no existe.");

            return wallet;
        }


        public async Task<Wallet> CreateWalletAsync(Wallet wallet)
        {
            wallet.CreatedAt = DateTime.UtcNow;
            wallet.UpdatedAt = DateTime.UtcNow;
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();
            return wallet;
        }

        public async Task<bool> UpdateWalletAsync(Wallet wallet)
        {
            try
            {
                var existingWallet = await _context.Wallets.FindAsync(wallet.Id);
                if (existingWallet == null)
                    throw new KeyNotFoundException("Billetera no encontrada.");

                existingWallet.Balance = wallet.Balance;
                existingWallet.Name = wallet.Name;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la billetera con ID {Id}", wallet.Id);
                return false;
            }
        }


        public async Task<bool> DeleteWalletAsync(int id)
        {
            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet == null) return false;

            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TransferBalanceAsync(int fromWalletId, int toWalletId, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("El monto de la transferencia debe ser mayor a cero.");

            var fromWallet = await _context.Wallets.FindAsync(fromWalletId);
            var toWallet = await _context.Wallets.FindAsync(toWalletId);

            if (fromWallet == null)
                throw new KeyNotFoundException($"La billetera de origen con ID {fromWalletId} no existe.");

            if (toWallet == null)
                throw new KeyNotFoundException($"La billetera de destino con ID {toWalletId} no existe.");

            if (fromWallet.Balance < amount)
                throw new InvalidOperationException("Saldo insuficiente para realizar la transferencia.");

            fromWallet.Balance -= amount;
            toWallet.Balance += amount;
            fromWallet.UpdatedAt = DateTime.UtcNow;
            toWallet.UpdatedAt = DateTime.UtcNow;

            _context.Transactions.Add(new Transaction
            {
                WalletId = fromWalletId,
                Amount = -amount,
                Type = "Débito",
                CreatedAt = DateTime.UtcNow
            });

            _context.Transactions.Add(new Transaction
            {
                WalletId = toWalletId,
                Amount = amount,
                Type = "Crédito",
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
