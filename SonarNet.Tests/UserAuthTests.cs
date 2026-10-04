using SonarNet.Services;

namespace SonarNet.Tests;

public class UserAuthTests
{
    private readonly UserRepository _repository = new();
    private readonly UserAuth _auth;

    public UserAuthTests()
    {
        _repository.Add("admin", "Адміністратор", "admin@sonarnet.local");
        _auth = new UserAuth(_repository);
        _auth.SetPassword("admin", "Adm1n#Secret");
    }

    [Fact]
    public void Authenticate_CorrectPassword_ReturnsTrue()
    {
        Assert.True(_auth.Authenticate("admin", "Adm1n#Secret"));
    }

    [Fact]
    public void Authenticate_WrongPassword_ReturnsFalse()
    {
        Assert.False(_auth.Authenticate("admin", "wrong-password"));
    }

    [Fact]
    public void Authenticate_UnknownLogin_ReturnsFalseWithoutException()
    {
        // Регресійний тест для hotfix 1.0.1
        Assert.False(_auth.Authenticate("guest", "any-password"));
    }

    [Fact]
    public void Authenticate_ThreeFailedAttempts_LocksAccount()
    {
        for (int i = 0; i < UserAuth.MaxFailedAttempts; i++)
            _auth.Authenticate("admin", "wrong-password");

        Assert.True(_auth.IsLocked("admin"));
        Assert.False(_auth.Authenticate("admin", "Adm1n#Secret"));
    }

    [Fact]
    public void SetPassword_TooShort_Throws()
    {
        Assert.Throws<ArgumentException>(() => _auth.SetPassword("admin", "123"));
    }

    [Fact]
    public void SetPassword_UnknownUser_Throws()
    {
        Assert.Throws<InvalidOperationException>(() => _auth.SetPassword("nobody", "LongEnough#1"));
    }
}