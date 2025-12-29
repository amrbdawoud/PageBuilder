namespace PageBuilder.Application.Validators;

using FluentValidation;
using PageBuilder.Application.DTOs;
using PageBuilder.Domain.Enums;

public class UpdateBlockDtoValidator : AbstractValidator<UpdateBlockDTO>
{
    public UpdateBlockDtoValidator()
    {
        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("Block type must be a valid BlockType enum value");

        RuleFor(x => x.Content).NotEmpty().WithMessage("Block content is required");

        RuleFor(x => x.Order).GreaterThanOrEqualTo(0).WithMessage("Order must be non-negative");
    }
}
