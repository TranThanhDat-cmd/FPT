using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Infrastructure.Definitions;

namespace Infrastructure.Modules.Users.Requests
{
    public class UpdateUserRequest
    {
        public string? FullName { get; set; }
        //public string? Avatar { get; set; }
    }

    public class UpdateUserRequestValidatior : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidatior()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage(Messages.Users.FullNameIsRequired);
        }
    }
}
