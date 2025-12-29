namespace PageBuilder.Application.Validators;

using FluentValidation;
using PageBuilder.Application.DTOs;

public class CreatePageDtoValidator : AbstractValidator<CreatePageDTO>
{
    public CreatePageDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Page title is required")
            .MaximumLength(300)
            .WithMessage("Page title must not exceed 300 characters");

        RuleFor(x => x.CompanyId).GreaterThan(0).WithMessage("Valid company ID is required");
    }
}
