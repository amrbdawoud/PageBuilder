namespace PageBuilder.Application.Validators;

using FluentValidation;
using PageBuilder.Application.DTOs;

public class UpdatePageDtoValidator : AbstractValidator<UpdatePageDTO>
{
    public UpdatePageDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Page title is required")
            .MaximumLength(300)
            .WithMessage("Page title must not exceed 300 characters");
    }
}
