using BestPost.Service.Common.Helpers;
using BestPost.Service.Dtos.Posts;
using FluentValidation;

namespace BestPost.Service.Validators.Posts;


public class PostCreateValidator : AbstractValidator<PostCreateDto>
{
    public PostCreateValidator()
    {
        RuleFor(dto => dto.Title).NotNull().NotEmpty().WithMessage("Name field is required!")
        .MinimumLength(3).WithMessage("Name must be more than 3 characters");

        RuleFor(dto => dto.Description).NotNull().NotEmpty().WithMessage("Description field is required!")
           .MinimumLength(20).WithMessage("Description field is required!");

        int maxImageSizeMB = 5;
        RuleFor(dto => dto.Image).NotEmpty().NotNull().WithMessage("Image field is required");
        RuleFor(dto => dto.Image.Length).LessThan(maxImageSizeMB * 1024 * 1024 + 1).WithMessage($"Image size must be less than {maxImageSizeMB} MB");
        RuleFor(dto => dto.Image.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not image file");
    }
}
