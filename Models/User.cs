namespace SonarNet.Models;

public class User
{
    public int Id { get; init; }
    public string Login { get; init; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public override string ToString() => $"#{Id} {Login} ({FullName})";
}