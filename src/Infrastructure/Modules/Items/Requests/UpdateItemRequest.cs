using FluentValidation;
using Infrastructure.Definitions;
using Infrastructure.Modules.Items.Enums;

namespace Infrastructure.Modules.Items.Requests;

public class UpdateItemRequest
{
    public string? Name { get; set; }
    public int Order { get; set; }
    public ItemType? ItemType { get; set; }


}

public class UpdateItemRequestValidator : AbstractValidator<UpdateItemRequest>
{
    public UpdateItemRequestValidator()
    {
        //RuleFor(x => x.Name).NotEmpty().WithMessage(Messages.Items.NameIsRequired);
        //RuleFor(x => x.Order).NotNull().WithMessage(Messages.Items.OrderIsRequired);
        RuleFor(x => x.ItemType).IsInEnum().WithMessage(Messages.Items.ItemTypeIsRequired);
        }
}