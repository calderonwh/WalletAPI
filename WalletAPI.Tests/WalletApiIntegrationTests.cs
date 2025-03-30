using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;

public class WalletApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public WalletApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_CreateWallet_ShouldReturnSuccess()
    {
        var newWallet = new { Name = "Test Wallet", Balance = 100 };
        var content = new StringContent(JsonConvert.SerializeObject(newWallet), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/wallets", content);

        response.EnsureSuccessStatusCode();
    }
}
