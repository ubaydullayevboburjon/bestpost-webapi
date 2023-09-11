using BestPost.DataAccsess.Interfaces.Users;
using BestPost.Domain.Entites.Users;
using BestPost.Domain.Exceptions.Users;
using BestPost.Service.Common.Helpers;
using BestPost.Service.Dtos.Users;
using BestPost.Service.Interfaces.Auth;
using BestPost.Service.Interfaces.Users;

namespace BestPost.Service.Services.Users;

public class UserService : IUserService
{
    private IUserRepository _repository;
    private IIdentityService _identity;

    public UserService(IUserRepository userRepository, IIdentityService identityService)
    {
        this._repository = userRepository;
        this._identity = identityService;
    }

    public async Task<long> CountAsync() => await _repository.CountAsync();
    
    public async Task<bool> DeleteAsync(long userId)
    {
        if (userId != _identity.UserId ) throw new UserNotFoundExcaption();
        var result = await _repository.DeleteAsync(userId);
        return result > 0;
    }

    public async Task<IList<User>> GetAllAsync()
    {
        var users = await _repository.GetAll();
        if (users is null) throw new UserNotFoundExcaption();
        else return users;
    }

    public async Task<User> GetByIdAsync(long userId)
    {
        var user = await _repository.GetByIdAsync(userId);
        if (user is null) throw new UserNotFoundExcaption();
        else return user;
    }

    public async Task<bool> UpdateAsync(long userId, UserUpdateDto dto)
    {
        var user = await _repository.GetByIdAsync(userId);
        if (user is null) throw new UserNotFoundExcaption();
        if (userId != _identity.UserId ) throw new UserNotFoundExcaption();

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;

        user.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(userId, user);
        return dbResult > 0;
    }
}

