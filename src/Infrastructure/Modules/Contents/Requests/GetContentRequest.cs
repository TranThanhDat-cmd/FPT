using Infrastructure.Modules.Contents.Enums;
using Infrastructure.Modules.Items.Enums;
using Infrastructure.Utilities;

namespace Infrastructure.Modules.Contents.Requests;

public class GetContentRequest : PaginationRequest
{
    public ContentType? ContentType { get; set; }
}