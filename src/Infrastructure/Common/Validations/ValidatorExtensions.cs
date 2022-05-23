using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Common.Validations;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, IFormFile?> IsValidContentType<T>(this IRuleBuilder<T, IFormFile?> ruleBuilder, ICollection<string> contentTypes)
    {
        return ruleBuilder.Must(file =>
        {
            if (file is null) 
                return true;
            return contentTypes.Any(x => file.ContentType.StartsWith(x));
        });
    }

}
