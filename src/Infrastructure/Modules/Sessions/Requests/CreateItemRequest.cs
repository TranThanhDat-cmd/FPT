using FluentValidation;
using Infrastructure.Definitions;
using Infrastructure.Modules.Items.Enums;

namespace Infrastructure.Modules.Sessions.Requests;

public class CreateItemRequest : BaseActionItemRequest
{
    

}


public class CreateItemsRequestValidator : AbstractValidator<List<CreateItemRequest>>
{
    public CreateItemsRequestValidator()
    {
        RuleFor(x => x)
            .Must(x => x.GroupBy(y => y.RoundType).Count() == 1)
            .WithMessage(Messages.Items.RoundTypeIsMany);
        RuleForEach(x => x)
            .SetValidator(new BaseActionItemRequestValidator());
    }
}
