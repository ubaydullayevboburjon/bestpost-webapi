using BestPost.Domain.Entites.Users;
using BestPost.Service.Dtos.Users;

namespace BestPost.Service.Interfaces.Users;

public interface IUserService
{
    public Task<bool> DeleteAsync(long userId);

    public Task<long> CountAsync();

    public Task<IList<User>> GetAllAsync();

    public Task<User> GetByIdAsync(long userId);

    public Task<bool> UpdateAsync(long userId, UserUpdateDto dto);
}
