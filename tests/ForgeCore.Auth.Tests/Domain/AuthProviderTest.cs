namespace ForgeCore.Auth.Tests.Domain;

public class AuthProviderTest
{
    [Fact]
    public void Create_Should_Assign_Id()
    {
        // Arrange
        var provider = new AuthProvider(type: AuthProviderType.EmailPassword, providerUserId: "example@test.com", email: "password123_");

        // Assert
        provider.Id.Should().NotBeEmpty("because auth provider constructor must set a new random guid");
    }

    [Fact]
    public void Create_Should_Assign_Provider()
    {
        // Arrange
        var provider = new AuthProvider(type: AuthProviderType.EmailPassword, providerUserId: "example@test.com", email: "password123_");

        // Assert
        provider.Type.Should().Be(AuthProviderType.EmailPassword, "because constructor must set the auth provider specified");
    }

    [Fact]
    public void Create_Should_Assign_ProviderUserId()
    {
        // Arrange
        var provider = new AuthProvider(type: AuthProviderType.EmailPassword, providerUserId: "example@test.com", email: "password123_");

        // Assert
        provider.ProviderUserId.Should().NotBeEmpty("constructor must set the provider user id specified");
    }

    [Fact]
    public void Create_Should_Assign_Email()
    {
        // Arrange
        var provider = new AuthProvider(type: AuthProviderType.EmailPassword, providerUserId: "example@test.com", email: "password123_");

        // Assert
        provider.Email.Should().NotBeEmpty("constructor must set the email specified");
    }
}
