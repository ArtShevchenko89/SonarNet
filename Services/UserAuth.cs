using System.Security.Cryptography;
using System.Text;

namespace SonarNet.Services;

/// <summary>
/// Базова аутентифікація користувачів: реєстрація облікових даних,
/// перевірка пароля та блокування після кількох невдалих спроб.
/// </summary>
public class UserAuth
{
    public const int MaxFailedAttempts = 3;
    public const int MinPasswordLength = 8;

    private readonly UserRepository _repository;
    private readonly Dictionary<string, (string Salt, string Hash)> _credentials = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _failedAttempts = new(StringComparer.OrdinalIgnoreCase);

    public UserAuth(UserRepository repository) => _repository = repository;

    /// <summary>Зберігає пароль користувача у вигляді солоного хешу SHA-256.</summary>
    public void SetPassword(string login, string password)
    {
        if (_repository.FindByLogin(login) is null)
            throw new InvalidOperationException($"Користувача '{login}' не знайдено");
        if (password is null || password.Length < MinPasswordLength)
            throw new ArgumentException($"Пароль має містити щонайменше {MinPasswordLength} символів", nameof(password));

        var salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
        _credentials[login] = (salt, ComputeHash(password, salt));
        _failedAttempts[login] = 0;
    }

    /// <summary>Перевіряє логін і пароль. Повертає true, якщо вхід успішний.</summary>
    public bool Authenticate(string login, string password)
    {
        if (IsLocked(login))
            return false;

        if (string.IsNullOrEmpty(login) || !_credentials.TryGetValue(login, out var credentials))
            return false;

        var (salt, hash) = credentials;
        bool ok = ComputeHash(password, salt) == hash;

        _failedAttempts[login] = ok ? 0 : _failedAttempts.GetValueOrDefault(login) + 1;
        return ok;
    }

    public bool IsLocked(string login) => _failedAttempts.GetValueOrDefault(login) >= MaxFailedAttempts;

    private static string ComputeHash(string password, string salt)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(salt + password));
        return Convert.ToHexString(bytes);
    }
}