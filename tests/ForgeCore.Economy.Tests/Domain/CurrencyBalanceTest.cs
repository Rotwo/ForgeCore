namespace ForgeCore.Economy.Tests.Domain;

public class CurrencyBalanceTest
{
    [Fact]
    public void Create_Should_Assign_Id()
    {
        // Arrange
        var currencyBalance = CurrencyBalanceFactory.Create();

        // Assert
        currencyBalance.Id.Should().NotBeEmpty("because the constructor must generate the id");
    }

    [Fact]
    public void Create_Should_Assign_Currency_Id()
    {
        // Arrange
        var currencyBalance = CurrencyBalanceFactory.Create();

        // Assert
        currencyBalance.CurrencyId.Should().NotBeEmpty("because the constructor must set the specified currency id");
    }

    [Fact]
    public void Create_Should_Assign_Wallet_Id()
    {
        // Arrange
        var currencyBalance = CurrencyBalanceFactory.Create();

        // Assert
        currencyBalance.WalletId.Should().NotBeEmpty("because the constructor must set the specified wallet id");
    }
}
