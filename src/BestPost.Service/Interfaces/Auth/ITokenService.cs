using BestPost.Domain.Entites.Users;

namespace BestPost.Service.Interfaces.Auth;

public interface ITokenService
{
    public string GenerateToken(User user);
}
