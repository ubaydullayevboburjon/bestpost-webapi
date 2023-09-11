using BestPost.DataAccsess.Interfaces.Users;
using BestPost.Domain.Entites.Users;
using BestPost.Domain.Exceptions.Auth;
using BestPost.Domain.Exceptions.Users;
using BestPost.Service.Common.Helpers;
using BestPost.Service.Common.Security;
using BestPost.Service.Dtos.Auth;
using BestPost.Service.Dtos.Notifications;
using BestPost.Service.Dtos.Security;
using BestPost.Service.Interfaces.Auth;
using BestPost.Service.Interfaces.Notifications;
using Microsoft.Extensions.Caching.Memory;

namespace BestPost.Service.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IUserRepository _repository;
    private readonly IEmailSender _emailSender;
    private readonly ITokenService _tokenService;
    private const int CACHED_MINUTES_FOR_REGISTER = 60;
    private const int CACHED_MINUTES_FOR_VERIFICATION = 5;
    private const string REGISTER_CACHE_KEY = "register_";
    private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;

    public AuthService(IMemoryCache memoryCache, IUserRepository userRepository,
       IEmailSender emailSender, ITokenService tokenService)
    {
        this._memoryCache = memoryCache;
        this._repository = userRepository;
        this._emailSender = emailSender;
        this._tokenService = tokenService;

    }
    public async Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto)
    {
        var user = await _repository.GetByEmailAsync(loginDto.Email);
        if (user is null) throw new UserNotFoundExcaption();

        var hasherResult = PasswordHasher.Verify(loginDto.Password, user.PasswordHash, user.PasswordSalt);
        if (hasherResult == false) throw new PasswordIncorrectException();

        string token = _tokenService.GenerateToken(user);
        return (Result: true, Token: token);
    }

    public async Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto)
    {
        var User = await _repository.GetByEmailAsync(dto.Email);
        if (User is not null) throw new UserAlreadyExistsExcaption(dto.Email);

        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + dto.Email, out RegisterDto cachedRegisterDto))
        {
            cachedRegisterDto.FirstName = cachedRegisterDto.FirstName;
            _memoryCache.Remove(dto.Email);
        }
        else _memoryCache.Set(REGISTER_CACHE_KEY + dto.Email, dto,
            TimeSpan.FromMinutes(CACHED_MINUTES_FOR_REGISTER));

        return (Result: true, CachedMinutes: CACHED_MINUTES_FOR_REGISTER);

    }

    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string email)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + email, out RegisterDto? registerDto))
        {
            VertificationDto verificationDto = new VertificationDto();
            verificationDto.Attempt = 0;
            verificationDto.CreatedAt = TimeHelper.GetDateTime();

            verificationDto.Code = CodeGenerator.GenerateRandomNumber();

            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + email, out VertificationDto? oldVerifcationDto))
            {
                _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + email);
            }

            _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + email, verificationDto,
                TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

            EmailMessage emailMessage = new EmailMessage();
            emailMessage.Title = "Best Post ";
            emailMessage.Content = "Your verification code : " + verificationDto.Code;
            emailMessage.Recipent = email;

            var emailResult = await _emailSender.SendAsync(emailMessage);
            if (emailResult is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
            else return (Result: false, CachedVerificationMinutes: 0);
        }
        else throw new UserCacheDataExpiredException();
    }

    public async  Task<(bool Result, string Token)> VerifyRegisterAsync(string email, int code)
    {
        var User = await _repository.GetByEmailAsync(email);
        if (User is not null) throw new UserAlreadyExistsExcaption(email);

        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + email, out RegisterDto registerDto))
        {
            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + email, out VertificationDto verificationDto))
            {
                if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
                    throw new VerificationTooManyRequestsException();
                else if (verificationDto.Code == code)
                {
                    var dbResult = await RegisterToDatabaseAsync(registerDto);
                    if (dbResult is true)
                    {
                        var user = await _repository.GetByEmailAsync(email);
                        string token = _tokenService.GenerateToken(user);
                        return (Result: true, Token: token);
                    }
                    return (Result: dbResult, Token: "");
                }
                else
                {
                    _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + email);
                    verificationDto.Attempt++;
                    _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + email, verificationDto,
                        TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));
                    return (Result: false, Token: "");
                }
            }
            else throw new VerificationCodeExpiredException();
        }
        else throw new UserCacheDataExpiredException();
    }

    private async Task<bool> RegisterToDatabaseAsync(RegisterDto registerDto)
    {
        var user = new User();
        user.FirstName = registerDto.FirstName;
        user.LastName = registerDto.LastName;
        user.Email = registerDto.Email;
        user.EmailConfirmed = true;

        var hasherResult = PasswordHasher.Hash(registerDto.Password);
        user.PasswordHash = hasherResult.Hash;
        user.PasswordSalt = hasherResult.Salt;

        user.CreatedAt = user.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.CreateAsync(user);
        return dbResult > 0;
    }
}

