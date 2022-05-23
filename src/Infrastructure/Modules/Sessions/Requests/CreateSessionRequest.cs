using FluentValidation;
using Infrastructure.Definitions;


namespace Infrastructure.Modules.Sessions.Requests;

public class CreateSessionRequest
{
    public string? Name { get; set; }
    public List<CreateSessionItemRequest>? Items { get; set; }
}

public class CreateSessionRequestValidator : AbstractValidator<CreateSessionRequest>
{
    public CreateSessionRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(Messages.Sessions.NameIsRequired);
        RuleForEach(x => x.Items).SetValidator(new CreateSessionItemRequestValidator());
    }
}