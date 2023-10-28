using BestPost.DataAccsess.Interfaces.Users;
using BestPost.DataAccsess.ViewModels;
using BestPost.Domain.Entites.Users;
using BestPost.Domain.Exceptions.Auth;
using BestPost.Domain.Exceptions.Files;
using BestPost.Domain.Exceptions.Users;
using BestPost.Service.Common.Helpers;
using BestPost.Service.Common.Security;
using BestPost.Service.Dtos.Users;
using BestPost.Service.Interfaces.Auth;
using BestPost.Service.Interfaces.Common;
using BestPost.Service.Interfaces.Users;

namespace BestPost.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IIdentityService _identity;
    private readonly IFileService _fileService;
    public UserService(IUserRepository userRepository, IIdentityService identityService, IFileService fileService)
    {
        this._repository = userRepository;
        this._identity = identityService;
        this._fileService = fileService;
    }

    public async Task<long> CountAsync() => await _repository.CountAsync();

    public async Task<bool> DeleteAsync(long userId)
    {
        if (userId != _identity.UserId) throw new UserNotFoundExcaption();
        var result = await _repository.DeleteAsync(userId);
        return result > 0;
    }

    public async Task<IList<UserViewModel>> GetAllAsync()
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

    public async Task<bool> UpdateAsync(UserUpdateDto dto)
    {

        var user = await _repository.GetByIdAsync(_identity.UserId);
        if (user is null) throw new UserNotFoundExcaption();

        var Username = await _repository.GetByUsernamAsync(_identity.Username);
        if (Username is null) throw new UsernameAlreadyExcaptions(_identity.Username);

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Username = dto.Username;
        user.UpdatedAt = TimeHelper.GetDateTime();


        user.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(user.Id, user);
        return dbResult > 0;
    }

    public async Task<bool> ForgotPasswordAsync(UserResetPasswordDto dto)
    {
        var user = await _repository.GetByIdAsync(_identity.UserId);
        if (user is null) throw new UserNotFoundExcaption();
        var hasherResult = PasswordHasher.Hash(dto.Password);
        user.PasswordHash = hasherResult.Hash;
        user.PasswordSalt = hasherResult.Salt;
        user.UpdatedAt = TimeHelper.GetDateTime();
        var result = await _repository.UpdateAsync(user.Id, user);

        return result > 0;
    }

    public async Task<bool> DeleteImageAsync()
    {
        var user = await _repository.GetByIdAsync(_identity.UserId);
        if (user is null) throw new UserNotFoundExcaption();
        var fileResult = await _fileService.DeleteImageAsync(user.ImagePath);
        if (fileResult) user.ImagePath = "";
        user.UpdatedAt = TimeHelper.GetDateTime();
        var result = await _repository.UpdateAsync(user.Id, user);

        return result > 0;
    }

    public async Task<bool> UploadImageAsync(UploadImageDto file)
    {
        var userviewmodel = await _repository.GetByIdAsync(_identity.UserId);
        if (userviewmodel is null) throw new UserNotFoundExcaption();

        if (file.Image is not null && userviewmodel.ImagePath != "")
        {
            var deleteResult = await _fileService.DeleteImageAsync(userviewmodel.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();
        }

        var fileResult = await _fileService.UploadImageAsync(file.Image!, "users");
        if (fileResult is not null) userviewmodel.ImagePath = fileResult;
        userviewmodel.UpdatedAt = TimeHelper.GetDateTime();
        var result = await _repository.UpdateAsync(userviewmodel.Id, userviewmodel);

        return result > 0;
    }
    public async Task<User> GetProfileInfoAsync()
    {
        var result = await _repository.GetByIdAsync(_identity.UserId);
        if (result is null) throw new UserNotFoundExcaption();

        return result;
    }


    public Task<bool> TokenCheker(string token)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ResetPassword(ResetPasswordDto dto)
    {
        var user = await _repository.GetByEmailAsync(_identity.Email);
        if (user is null) throw new UserNotFoundExcaption();

        var hasherResult = PasswordHasher.Verify(dto.OldPassword, user.PasswordHash, user.PasswordSalt);
        if (hasherResult == false) throw new PasswordIncorrectException();

        var hashResult = PasswordHasher.Hash(dto.NewPassword);
        user.PasswordHash = hashResult.Hash;
        user.PasswordSalt = hashResult.Salt;
        var dbResult = await _repository.UpdateAsync(user.Id, user);

        return dbResult > 0;
    }
}
