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
    public class UpdateContentRequest
    {
        public Guid Id { get; set; }
        public ContentType? ContentType { get; set; }
        public string? Suggest { get; set; }
    }

    public class UpdateContentRequestValidator : AbstractValidator<UpdateContentRequest>
    {
        public UpdateContentRequestValidator()
        {
            RuleFor(x => x.ContentType).IsInEnum().WithMessage(Messages.Contents.ItemTypeIsRequired);
        }
    }
}
