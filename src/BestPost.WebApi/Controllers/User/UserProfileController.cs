using BestPost.Service.Dtos.Users;
using BestPost.Service.Interfaces.Users;
using BestPost.Service.Validators.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestPost.WebApi.Controllers.User;

[Route("api/user")]
[ApiController]
[Authorize(Roles = "User")]
public class UserProfileController : ControllerBase
{
    IUserService _userService;

    public UserProfileController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("count")]

    public async Task<IActionResult> CountAsync() => Ok(await _userService.CountAsync());

    [HttpGet("users")]

    public async Task<IActionResult> GetAllAsync() => Ok(await _userService.GetAllAsync());

    [HttpGet("profile")]

    public async Task<IActionResult> GetPofileInfoAsync() => Ok(await _userService.GetProfileInfoAsync());

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync([FromForm] UserUpdateDto dto)
    {
        var updateValidator = new UserUpdateValidator();
        var result = updateValidator.Validate(dto);
        if (result.IsValid) return Ok(await _userService.UpdateAsync(dto));
        else return BadRequest(result.Errors);
    }

    [HttpPut("upload/image")]
    public async Task<IActionResult> UploadImageAsync([FromForm] UploadImageDto file)
    {
        var validator = new UploadImageValidator();
        var valResult = validator.Validate(file);
        if (valResult.IsValid == false) return BadRequest(valResult.Errors);

        return Ok(await _userService.UploadImageAsync(file));
    }

    [HttpPut("delete/image")]

    public async Task<IActionResult> DeleteImageAsync() => Ok(await _userService.DeleteImageAsync());

}
