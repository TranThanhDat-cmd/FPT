using FluentValidation;
using Infrastructure.Definitions;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Modules.Users.Requests;

public class LoginRequest
{
    public string? EmailAddress { get; set; }
    public string? Password { get; set; }
}

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator(IRepositoryWrapper repositoryWrapper)
    {
        RuleFor(x => x.EmailAddress).NotEmpty().WithMessage(Messages.Users.EmailIsRequired)
            .EmailAddress().WithMessage(Messages.Users.EmailBadFormat);
        RuleFor(x => x.Password).NotEmpty().WithMessage(Messages.Users.PasswordIsRequired);
    }
}