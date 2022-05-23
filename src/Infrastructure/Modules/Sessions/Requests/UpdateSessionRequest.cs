using FluentValidation;
using Infrastructure.Definitions;

namespace Infrastructure.Modules.Sessions.Requests;

public class UpdateSessionRequest
{
    public string? Name { get; set; }
}
public class UpdateSessionRequestValidator : AbstractValidator<UpdateSessionRequest>
{
    public UpdateSessionRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(Messages.Sessions.NameIsRequired);
    }
}