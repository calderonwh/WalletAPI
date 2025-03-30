using WalletAPI.Domain.Entities;

namespace WalletAPI.Application.Interfaces
{
    public interface IWalletService
    {
        Task<IEnumerable<Wallet>> GetAllWalletsAsync();
        Task<Wallet?> GetWalletByIdAsync(int id);
        Task<Wallet> CreateWalletAsync(Wallet wallet);
        Task<bool> UpdateWalletAsync(Wallet wallet);
        Task<bool> DeleteWalletAsync(int id);
        Task<bool> TransferBalanceAsync(int fromWalletId, int toWalletId, decimal amount);
    }
}
