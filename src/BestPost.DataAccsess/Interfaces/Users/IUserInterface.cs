using BestPost.Domain.Entites.Users;

namespace BestPost.DataAccsess.Interfaces.Users;

public interface IUserInterface
{
    public Task<User> GetByUsername(string username);

}
