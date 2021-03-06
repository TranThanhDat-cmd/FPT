using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Definitions
{
    public struct JwtClaimsName
    {
        public const string Identification = "id";
        public const string PhoneNumber = "phone";
        public const string FullName = "full_name";
        public const string BirthDay = "birthday";
        public const string UserName = "user_name";
        public const string EmailAddress = "email";
        public const string Gender = "gender";
        public const string Avatar = "avatar";
        public const string UserPermissions = "user_permissions";
    }
}
