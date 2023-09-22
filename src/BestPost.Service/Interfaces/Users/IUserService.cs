using BestPost.DataAccsess.ViewModels;
using BestPost.Domain.Entites.Users;
using BestPost.Service.Dtos.Users;

namespace BestPost.Service.Interfaces.Users;

public interface IUserService
{
    public Task<bool> DeleteAsync(long userId);

    public Task<long> CountAsync();

    public Task<IList<UserViewModel>> GetAllAsync();

    public Task<User> GetByIdAsync(long userId);

    public Task<bool> UpdateAsync(UserUpdateDto dto);

    public Task<bool> DeleteImageAsync();

    public Task<bool> ResetPasswordAsync(UserResetPasswordDto dto);

    public Task<bool> UploadImageAsync(UploadImageDto file);

    public Task<User> GetProfileInfoAsync();
    public Task<bool> TokenCheker(string token);
}
