using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Infrastructure.Definitions;
using Infrastructure.Modules.Items.Enums;

namespace Infrastructure.Modules.Sessions.Requests
{
    public class UpdateItemsRequest : BaseActionItemRequest
    {

    }

    public class UpdateItemsRequestValidator : AbstractValidator<List<UpdateItemsRequest>>
    {
        public UpdateItemsRequestValidator()
        {

            RuleFor(x => x)
                .Must(x => x.GroupBy(y => y.RoundType).Count() == 1)
                .WithMessage(Messages.Items.RoundTypeIsMany);
            RuleForEach(x => x)
                .SetValidator(new BaseActionItemRequestValidator());
        }
    }
    
}
