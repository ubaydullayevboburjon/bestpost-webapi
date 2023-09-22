using BestPost.DataAccsess.ViewModels;
using BestPost.Domain.Entites.Users;

namespace BestPost.DataAccsess.Interfaces.Users;

public interface IUserRepository : IRepository<User, UserViewModel>
{
    public Task<UserViewModel> GetByUsernamAsync(string username);

    public Task<User?> GetByEmailAsync(string email);

    public Task<List<UserViewModel>> GetAll();
}
