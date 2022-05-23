using FluentValidation;
using Infrastructure.Definitions;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Modules.Sessions.Requests;

public class CreateContentSessionRequest
{
    public Guid? ContentId { get; set; }
}

public class CreateContentSessionRequestValidator : AbstractValidator<List<CreateContentSessionRequest>>
{
    public CreateContentSessionRequestValidator(IRepositoryWrapper repositoryWrapper)
    {
        CascadeMode = CascadeMode.Stop;
        RuleForEach(x => x)
            .NotEmpty().WithMessage(Messages.Contents.IdIsRequired)
            .MustAsync(async (content, obj) => await repositoryWrapper.Contents.GetByIdAsync(content.ContentId) != null)
            .WithMessage(Messages.Contents.IdNotFound);
        RuleFor(x => x)
            .Must(x => x.GroupBy(y => y.ContentId).Count() == x.Count)
            .WithMessage(Messages.Contents.IdIsDuplicate);
    }
}