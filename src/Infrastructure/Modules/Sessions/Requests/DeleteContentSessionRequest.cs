using FluentValidation;
using Infrastructure.Definitions;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Modules.Sessions.Requests;

public class DeleteContentSessionRequest
{
    public List<Guid>? ContentIds { get; set; }

}

public class DeleteContentSessionRequestValidator : AbstractValidator<DeleteContentSessionRequest>
{
    public DeleteContentSessionRequestValidator(IRepositoryWrapper repositoryWrapper)
    {
        RuleForEach(x => x.ContentIds)
            .NotEmpty().WithMessage(Messages.ContentSessions.ContentIdIsRequired)
            .MustAsync(async (contentId, cancellationToken) => await repositoryWrapper.Contents.GetByIdAsync(contentId) != null)
            .WithMessage(Messages.Contents.IdNotFound);
    }
}