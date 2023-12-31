﻿using BestPost.Service.Common.Helpers;
using BestPost.Service.Dtos.Users;
using FluentValidation;

namespace BestPost.Service.Validators.Users;

public class UploadImageValidator : AbstractValidator<UploadImageDto>
{
    public UploadImageValidator()
    {
        int maxImageSizeMB = 5;
        RuleFor(dto => dto.Image).NotEmpty().NotNull().WithMessage("Image field is required");
        RuleFor(dto => dto.Image.Length).LessThan(maxImageSizeMB * 1024 * 1024 + 1)
            .WithMessage($"Image size must be less than {maxImageSizeMB} MB");
        RuleFor(dto => dto.Image.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not image file");
    }
}
