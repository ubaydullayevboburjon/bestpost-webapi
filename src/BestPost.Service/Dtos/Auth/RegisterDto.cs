﻿namespace BestPost.Service.Dtos.Auth;

public class RegisterDto
{
    public string FirstName { get; set; } = String.Empty;

    public string LastName { get; set; } = String.Empty;

    public string Username { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;

    public string Password { get; set; } = string.Empty;
}
