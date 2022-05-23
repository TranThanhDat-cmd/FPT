using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Infrastructure.Definitions;
using Infrastructure.Modules.Contents.Enums;
using Infrastructure.Modules.Items.Enums;

namespace Infrastructure.Modules.Contents.Requests
{
    public class CreateContentRequest
    {
        public ContentType? ContentType { get; set; }
        public string? Suggest { get; set; }
    }

    public class CreateContentRequestValidator : AbstractValidator<CreateContentRequest>
    {
        public CreateContentRequestValidator()
        {
            RuleFor(x => x.ContentType).NotNull().IsInEnum().WithMessage(Messages.Contents.ItemTypeIsRequired);
            RuleFor(x => x.Suggest).NotEmpty().WithMessage(Messages.Contents.SuggestIsRequired);
        }
    }
}

