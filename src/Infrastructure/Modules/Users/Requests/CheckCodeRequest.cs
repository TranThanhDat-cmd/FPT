using FluentValidation;
using Infrastructure.Definitions;

namespace Infrastructure.Modules.Users.Requests;

public class CheckCodeRequest
{
    public string? Code { get; set; }
    public string? EmailAddress { get; set; }
}

public class CheckCodeRequestValidator : AbstractValidator<CheckCodeRequest>
{
    public CheckCodeRequestValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage(Messages.Users.CodeIsRequired);
        RuleFor(x => x.EmailAddress).NotEmpty().WithMessage(Messages.Users.EmailIsRequired);
    }
}