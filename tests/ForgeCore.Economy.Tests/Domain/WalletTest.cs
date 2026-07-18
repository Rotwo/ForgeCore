namespace ForgeCore.Economy.Tests.Domain;

public class WalletTest
{
    [Fact]
    public void Create_Should_Assign_Id()
    {
        // Arrange
        var wallet = WalletFactory.Create();

        // Assert
        wallet.Id.Should().NotBeEmpty("because the constructor must generate a wallet id");
    }

    [Fact]
    public void Create_Should_Assign_Owner_Id()
    {
        // Arrange
        var wallet = WalletFactory.Create();

        // Assert
        wallet.OwnerId.Should().NotBeEmpty("because the constructor must assign the specified owner id");
    }

    [Fact]
    public void Create_Should_Assign_Creation_Date()
    {
        // Arrange
        var wallet = WalletFactory.Create();

        // Assert
        wallet.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void AddCurrencyBalance_Should_Add_Balance_To_Wallet()
    {
        // Arrange
        var wallet = WalletFactory.Create();
        var currencyBalance = CurrencyBalanceFactory.CreateWithWalletId(wallet.Id);

        // Act
        wallet.AddCurrencyBalance(currencyBalance);

        // Assert
        wallet.Balances.FirstOrDefault(b => b.Id == currencyBalance.Id).Should().NotBeNull("because the balances list must have registered the new currency balance");
    }

    [Fact]
    public void AddCurrencyBalance_Should_Throw_When_Currency_Already_Exists()
    {
        // Arrange
        var wallet = WalletFactory.Create();
        var currencyId = Guid.NewGuid();

        wallet.AddCurrencyBalance(new CurrencyBalance(currencyId: currencyId, walletId: wallet.Id));

        // Act
        Action act = () =>
            wallet.AddCurrencyBalance(new CurrencyBalance(currencyId: currencyId, walletId: wallet.Id));

        // Assert
        act.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("*Currency already exists*");
    }

    [Theory]
    [InlineData(100)]
    [InlineData(250)]
    [InlineData(500)]
    public void Deposit_Should_Increase_Balance(decimal amount)
    {
        // Arrange
        var wallet = WalletFactory.Create();
        var currencyBalance = CurrencyBalanceFactory.CreateWithWalletId(wallet.Id);

        wallet.AddCurrencyBalance(currencyBalance);

        // Act
        wallet.Deposit(amount: amount, currencyId: currencyBalance.CurrencyId);

        // Assert
        currencyBalance.Balance.Should().Be(amount);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(20)]
    [InlineData(30)]
    public void Withdraw_Should_Decrease_Balance(decimal amount)
    {
        decimal originalAmount = 100;

        // Arrange
        var wallet = WalletFactory.Create();
        var currencyBalance = CurrencyBalanceFactory.CreateWithWalletId(wallet.Id);

        wallet.AddCurrencyBalance(currencyBalance);
        currencyBalance.AddBalance(originalAmount);

        // Act
        wallet.Withdraw(amount: amount, currencyId: currencyBalance.CurrencyId);

        // Assert
        currencyBalance.Balance.Should().Be(originalAmount - amount);
    }

    public void Withdraw_Should_Throw_When_Balance_Is_Insufficient()
    {
        // Arrange
        var wallet = WalletFactory.Create();
        var currencyBalance = CurrencyBalanceFactory.CreateWithWalletId(wallet.Id);

        wallet.AddCurrencyBalance(currencyBalance);

        // Act
        Action act = () => wallet.Withdraw(amount: 200, currencyId: currencyBalance.Id);

        // Assert
        act.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("*Insufficient funds.*");
    }
}
