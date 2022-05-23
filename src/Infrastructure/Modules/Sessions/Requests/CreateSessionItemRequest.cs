using FluentValidation;
using Infrastructure.Definitions;
using Infrastructure.Modules.Items.Enums;

namespace Infrastructure.Modules.Sessions.Requests;

public class CreateSessionItemRequest
{
    public string? Name { get; set; }
    public int? Order { get; set; }
    public ItemType? ItemType { get; set; }
    public RoundType? RoundType { get; set; }

}

public class CreateSessionItemRequestValidator : AbstractValidator<CreateSessionItemRequest>
{
    public CreateSessionItemRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(Messages.Items.NameIsRequired);
        RuleFor(x => x.Order).NotNull().WithMessage(Messages.Items.OrderIsRequired);
        RuleFor(x => x.ItemType).IsInEnum().WithMessage(Messages.Items.ItemTypeIsInEnum);
        RuleFor(x => x.RoundType).IsInEnum().WithMessage(Messages.Items.RoundTypeIsInEnum);
    }
}