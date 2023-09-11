﻿namespace BestPost.Domain.Exceptions.Auth;
public class PasswordIncorrectException : BadRequestException
{
    public PasswordIncorrectException()
    {
        TitleMessage = "Password is invalid!";
    }
}
