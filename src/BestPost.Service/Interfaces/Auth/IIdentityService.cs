namespace BestPost.Service.Interfaces.Auth;

public interface IIdentityService
{
    public long UserId { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public string Username { get; }

    public string Email { get; }

    public string? IdentityRole { get; }

}
