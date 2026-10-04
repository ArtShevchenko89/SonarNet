using SonarNet.Services;

namespace SonarNet.Tests;

public class UserRepositoryTests
{
    [Fact]
    public void Add_AssignsSequentialIds()
    {
        var repo = new UserRepository();
        var first = repo.Add("u1", "User 1", "u1@test");
        var second = repo.Add("u2", "User 2", "u2@test");
        Assert.Equal(1, first.Id);
        Assert.Equal(2, second.Id);
        Assert.Equal(2, repo.Count);
    }

    [Fact]
    public void Add_DuplicateLogin_IsCaseInsensitive_Throws()
    {
        var repo = new UserRepository();
        repo.Add("admin", "Admin", "a@test");
        Assert.Throws<InvalidOperationException>(() => repo.Add("ADMIN", "Admin 2", "b@test"));
    }

    [Fact]
    public void FindByLogin_Unknown_ReturnsNull()
    {
        Assert.Null(new UserRepository().FindByLogin("missing"));
    }
}