using BestPost.Service.Dtos.Auth;
using FluentValidation;

namespace BestPost.Service.Validators.Auth;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(dto => dto.Email).Must(email => EmailValidator.IsValid(email))
            .WithMessage("Email addres is invalid! example@gmail.com");

        RuleFor(dto => dto.Password).Must(password => PasswordValidator.IsStrongPassword(password).IsValid)
            .WithMessage("Password is not strong password!");
    }
}
