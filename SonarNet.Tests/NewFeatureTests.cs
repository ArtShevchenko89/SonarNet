using SonarNet.Services;

namespace SonarNet.Tests;

public class NewFeatureTests
{
    private readonly NewFeature _feature = new();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("short")]
    [InlineData("alllowercase")]
    public void Evaluate_WeakPasswords_ReturnsWeak(string? password)
    {
        Assert.Equal(PasswordStrength.Weak, _feature.Evaluate(password));
    }

    [Fact]
    public void Evaluate_TwoCharacterClasses_ReturnsMedium()
    {
        Assert.Equal(PasswordStrength.Medium, _feature.Evaluate("password123"));
    }

    [Fact]
    public void Evaluate_LongMixedPassword_ReturnsStrong()
    {
        Assert.Equal(PasswordStrength.Strong, _feature.Evaluate("Str0ng#Passw0rd"));
    }

    [Fact]
    public void GetRecommendations_StrongPassword_ReturnsNoTips()
    {
        Assert.Empty(_feature.GetRecommendations("Str0ng#Passw0rd"));
    }

    [Fact]
    public void GetRecommendations_WeakPassword_ContainsAllTips()
    {
        var tips = _feature.GetRecommendations("abc");
        Assert.Equal(4, tips.Count);
    }
}