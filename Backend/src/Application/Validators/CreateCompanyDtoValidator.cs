namespace PageBuilder.Application.Validators;

using FluentValidation;
using PageBuilder.Application.DTOs;

public class CreateCompanyDtoValidator : AbstractValidator<CreateCompanyDTO>
{
    public CreateCompanyDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Company name is required")
            .MaximumLength(200)
            .WithMessage("Company name must not exceed 200 characters");
    }
}
