using System.Data;
using FluentValidation;
using Infrastructure.Definitions;
using Infrastructure.Utilities;
using Infrastructure.Modules.Contents.Enums;

namespace Infrastructure.Modules.Sessions.Requests;

public class GetContentBySessionIdRequest : PaginationRequest
{
    public ContentType? ContentType { get; set; }
}

public class GetContentBySessionIdRequestValidator : AbstractValidator<GetContentBySessionIdRequest>
{
    public GetContentBySessionIdRequestValidator()
    {
        
        RuleFor(x => x.ContentType)
            .IsInEnum().WithMessage(Messages.Contents.ContentTypeIsInEnum);
    }
}