using FluentValidation;
using Infrastructure.Modules.Items.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Definitions;

namespace Infrastructure.Modules.Sessions.Requests
{
    public class BaseActionItemRequest
    {

        public string? Name { get; set; }
        public int? Order { get; set; }
        public ItemType? ItemType { get; set; }
        public RoundType? RoundType { get; set; }
    }
    public class BaseActionItemRequestValidator : AbstractValidator<BaseActionItemRequest>
    {
        public BaseActionItemRequestValidator()
        {
            
            RuleFor(x => x.Order).NotNull().WithMessage(Messages.Items.OrderIsRequired);
            RuleFor(x => x.ItemType).IsInEnum().WithMessage(Messages.Items.ItemTypeIsRequired);
            RuleFor(x => x.RoundType).IsInEnum().WithMessage(Messages.Items.RoundTypeIsRequired);
        }
    }

}
