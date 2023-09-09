using BestPost.Service.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPost.Service.Interfaces.Auth;

public interface IAuthService
{
    public Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto);

    public Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string email);

    public Task<(bool Result, string Token)> VerifyRegisterAsync(string email, int code);

    public Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto);
}
