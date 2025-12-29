namespace PageBuilder.Application.Validators;

using FluentValidation;
using PageBuilder.Application.DTOs;

public class ReorderBlocksDtoValidator : AbstractValidator<ReorderBlocksDTO>
{
    public ReorderBlocksDtoValidator()
    {
        RuleFor(x => x.BlockIds)
            .NotNull()
            .WithMessage("Block IDs list is required")
            .Must(list => list.Count > 0)
            .WithMessage("At least one block ID is required")
            .Must(list => list.All(id => id > 0))
            .WithMessage("All block IDs must be positive integers");
    }
}
