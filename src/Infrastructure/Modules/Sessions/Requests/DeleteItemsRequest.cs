using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Infrastructure.Definitions;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Modules.Sessions.Requests
{
    public class DeleteItemsRequest
    {
        public List<Guid>? ItemIds { get; set; }
    }

    public class DeleteItemsRequestValidator : AbstractValidator<DeleteItemsRequest>
    {
        public DeleteItemsRequestValidator(IRepositoryWrapper repositoryWrapper)
        {
            RuleForEach(x => x.ItemIds)
                .NotEmpty().WithMessage(Messages.Items.IdIsRequired)
                .MustAsync(async (contentId, cancellationToken) => await repositoryWrapper.Items.GetByIdAsync(contentId) != null)
                .WithMessage(Messages.Items.IdNotFound);
        }

    }



}
