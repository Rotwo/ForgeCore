namespace ForgeCore.Players.Tests.Domain;

public class PlayerTest
{
    [Fact]
    public void Create_Should_Assign_Id()
    {
        // Arrange
        var player = PlayerFactory.Create();

        // Assert
        player.Id.Should().NotBeEmpty("because player constructor must generate a random guid");
    }

    [Fact]
    public void RenameNickname_Should_Update_Nickname()
    {
        // Arrange
        var player = PlayerFactory.Create();

        // Act
        player.Rename("Gamer");

        // Assert
        player.Nickname.Should().Be("Gamer", "because the rename function should set the stablished new nickname");
    }

    [Fact]
    public void Create_Should_Assign_AccountId()
    {
        // Arrange
        var player = PlayerFactory.Create();

        // Assert
        player.AccountId.Should().NotBeEmpty("because the constructor must set the account id");
    }
}