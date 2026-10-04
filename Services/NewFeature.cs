namespace SonarNet.Services;

/// <summary>Рівень надійності пароля.</summary>
public enum PasswordStrength
{
    Weak,
    Medium,
    Strong
}

public class NewFeature
{
    public PasswordStrength Evaluate(string? password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < UserAuth.MinPasswordLength)
            return PasswordStrength.Weak;

        int classes = 0;
        if (password.Any(char.IsLower)) classes++;
        if (password.Any(char.IsUpper)) classes++;
        if (password.Any(char.IsDigit)) classes++;
        if (password.Any(c => !char.IsLetterOrDigit(c))) classes++;

        if (classes >= 3 && password.Length >= 12)
            return PasswordStrength.Strong;
        return classes >= 2 ? PasswordStrength.Medium : PasswordStrength.Weak;
    }

    /// <summary>Повертає поради щодо посилення пароля.</summary>
    public IReadOnlyList<string> GetRecommendations(string? password)
    {
        var tips = new List<string>();
        password ??= string.Empty;
        if (password.Length < 12) tips.Add("Використайте щонайменше 12 символів");
        if (!password.Any(char.IsUpper)) tips.Add("Додайте великі літери");
        if (!password.Any(char.IsDigit)) tips.Add("Додайте цифри");
        if (!password.Any(c => !char.IsLetterOrDigit(c))) tips.Add("Додайте спеціальні символи");
        return tips;
    }
}