using BestPost.Domain.Entites.Users;

namespace BestPost.DataAccsess.Interfaces.Users;

public interface IUserRepository:IRepository<User,User>
{
    public Task<User> GetByUsername(string username);

    public Task<User?> GetByEmailAsync(string email);

    public Task<List<User>> GetAll();
}
