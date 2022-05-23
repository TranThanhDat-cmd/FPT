using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Infrastructure.Definitions;
using Infrastructure.Modules.Items.Enums;
using Infrastructure.Utilities;

namespace Infrastructure.Modules.Sessions.Requests
{
    public class GetItemBySessionIdRequest : PaginationRequest
    {
        public ItemType? ItemType { get; set; }
        public RoundType? RoundType { get; set; }
    }


    public class GetItemBySessionIdValidator : AbstractValidator<GetItemBySessionIdRequest>
    {
        public GetItemBySessionIdValidator()
        {
            
            RuleFor(x => x.ItemType)
                .IsInEnum().WithMessage(Messages.Items.ItemTypeIsInEnum);
            RuleFor(x => x.RoundType)
                .IsInEnum().WithMessage(Messages.Items.RoundTypeIsInEnum);
        }
    }
}
