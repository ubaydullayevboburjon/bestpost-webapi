using BestPost.Service.Dtos.Users;
using FluentValidation;

namespace BestPost.Service.Validators.Users;

public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateValidator()
    {
        RuleFor(dto => dto.FirstName).NotNull().NotEmpty().WithMessage("Firstname is required!")
            .MinimumLength(2).WithMessage("FirstName must be more than 2 characters")
            .MaximumLength(20).WithMessage("Firstname must be less than 20 characters");

        RuleFor(dto => dto.LastName).NotNull().NotEmpty().WithMessage("Lastname is required!")
            .MinimumLength(2).WithMessage("LastName must be more than 2 characters")
            .MaximumLength(20).WithMessage("Lastname must be less than 20 characters");

        RuleFor(dto => dto.Username).NotNull().NotEmpty().WithMessage("Username field is required!")
            .MinimumLength(4).WithMessage("Username must be more than 4 characters")
            .MaximumLength(32).WithMessage("Username must be less than 20 characters");
    }
}