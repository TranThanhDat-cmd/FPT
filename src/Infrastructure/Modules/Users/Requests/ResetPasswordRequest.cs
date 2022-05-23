using FluentValidation;
using Infrastructure.Definitions;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Modules.Users.Requests;

public class ResetPasswordRequest
{
    public string? EmailAddress { get; set; }
    public string? Code { get; set; }
    public string? Password { get; set; }
    public string? PasswordConfirm { get; set; }
}

public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(x => x.EmailAddress).NotEmpty().WithMessage(Messages.Users.EmailIsRequired)
            .EmailAddress().WithMessage(Messages.Users.EmailBadFormat); ;
        RuleFor(x => x.Password).NotEmpty().WithMessage(Messages.Users.PasswordIsRequired)
            .Length(6, 50).WithMessage(Messages.Users.PasswordOverLength);
        RuleFor(x => x.PasswordConfirm)
            .Equal(x => x.Password).WithMessage(Messages.Users.PasswordConfirmNotMatch);

    }
}