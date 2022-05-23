using FluentValidation;
using Infrastructure.Definitions;

namespace Infrastructure.Modules.Users.Requests;

public class ChangePasswordRequest
{
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
}

public class UpdatePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public UpdatePasswordRequestValidator()
    {
        RuleFor(x => x.OldPassword).NotEmpty().WithMessage(Messages.Users.OldPasswordIsRequired)
            .Length(6, 50).WithMessage(Messages.Users.PasswordOverLength); ;
        RuleFor(x => x.NewPassword).NotEmpty().WithMessage(Messages.Users.OldPasswordIsRequired)
            .Length(6, 50).WithMessage(Messages.Users.PasswordOverLength); ;

    }
}
