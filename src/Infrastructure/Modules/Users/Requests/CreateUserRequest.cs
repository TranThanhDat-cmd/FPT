using FluentValidation;
using Infrastructure.Definitions;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Modules.Users.Requests;

public class CreateUserRequest
{
    public string? EmailAddress { get; set; }
    public string? Password { get; set; }
    public string? FullName { get; set; }
   
}

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator(IRepositoryWrapper repositoryWrapper)
    {
        RuleFor(x => x.EmailAddress).NotEmpty().WithMessage(Messages.Users.EmailIsRequired).
            MustAsync(async (email, obj) => !await repositoryWrapper.Users.AnyAsync(x => x.EmailAddress == email)).WithMessage(Messages.Users.EmailIsExisting)
            .EmailAddress().WithMessage(Messages.Users.EmailBadFormat);;
        RuleFor(x => x.FullName).NotEmpty().WithMessage(Messages.Users.FullNameIsRequired);
        RuleFor(x => x.Password).NotEmpty().WithMessage(Messages.Users.PasswordIsRequired)
            .Length(6,50).WithMessage(Messages.Users.PasswordOverLength);
    }

}
