using SonarNet.Models;

namespace SonarNet.Services;

public class UserRepository
{
    private readonly List<User> _users = new();
    private int _nextId = 1;

    public User Add(string login, string fullName, string email)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentException("Логін не може бути порожнім", nameof(login));
        if (FindByLogin(login) is not null)
            throw new InvalidOperationException($"Користувач '{login}' вже існує");

        var user = new User { Id = _nextId++, Login = login, FullName = fullName, Email = email };
        _users.Add(user);
        return user;
    }

    public User? FindByLogin(string login) =>
        _users.FirstOrDefault(u => string.Equals(u.Login, login, StringComparison.OrdinalIgnoreCase));

    public IReadOnlyList<User> GetAll() => _users.AsReadOnly();

    public int Count => _users.Count;
}