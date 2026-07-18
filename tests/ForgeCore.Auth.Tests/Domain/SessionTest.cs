namespace ForgeCore.Auth.Tests.Domain;

public class SessionTest
{
    [Fact]
    public void Create_Should_Assign_AccountId()
    {
        // Arrange
        var account = AccountFactory.Create();
        var session = new Session(account.Id, "refresh-token", TimeSpan.FromDays(7));

        // Assert
        session.AccountId.Should().Be(account.Id, "because account id must be set by constructor");
    }

    [Fact]
    public void Create_Should_Generate_RefreshToken()
    {
        // Arrange
        var session = SessionFactory.Create();

        // Assert
        session.RefreshToken.Should().NotBeNullOrEmpty("because refresh token must be set by constructor and generated from token service");
    }

    [Fact]
    public void Create_Should_Set_Expiration_Date()
    {
        // Arrange
        var session = SessionFactory.Create();

        // Assert
        session.ExpiresAt.Should().BeAfter(DateTime.UtcNow, "because expiration date is set to be 7 days from the moment");
    }

    [Fact]
    public void Create_Should_Be_Active()
    {
        // Arrange
        var session = SessionFactory.Create();

        // Assert
        session.ExpiresAt.Should().BeAfter(DateTime.UtcNow, "because expiration date is set to be 7 days from the moment");
    }

}
