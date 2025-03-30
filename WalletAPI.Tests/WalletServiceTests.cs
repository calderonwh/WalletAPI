using WalletAPI.Application.Services; 
using WalletAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WalletAPI.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

public class WalletServiceTests
{
    private readonly WalletService _walletService;
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<WalletService> _logger;

    public WalletServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDB")
            .Options;
        _dbContext = new ApplicationDbContext(options);

        var mockLogger = new Mock<ILogger<WalletService>>();
        _logger = mockLogger.Object;

        _walletService = new WalletService(_dbContext, _logger);
    }

    [Fact]
    public async Task CreateWalletAsync_ShouldCreateWallet()
    {
        var wallet = new Wallet { Name = "Test Wallet", Balance = 100 };

        var result = await _walletService.CreateWalletAsync(wallet);

        Assert.NotNull(result);
        Assert.Equal("Test Wallet", result.Name);
        Assert.Equal(100, result.Balance);
    }

    [Fact]
    public async Task CreateWalletAsync_ShouldFail_WhenWalletNameIsEmpty()
    {
        var wallet = new Wallet { Name = "", Balance = 50 };

        await Assert.ThrowsAsync<ArgumentException>(() => _walletService.CreateWalletAsync(wallet));
    }

    [Fact]
    public async Task TransferBalanceAsync_ShouldFail_WhenInsufficientBalance()
    {
        var fromWallet = await _walletService.CreateWalletAsync(new Wallet { Name = "Sender", Balance = 50 });
        var toWallet = await _walletService.CreateWalletAsync(new Wallet { Name = "Receiver", Balance = 100 });

        var result = await _walletService.TransferBalanceAsync(fromWallet.Id, toWallet.Id, 200);

        Assert.False(result);
    }


}
