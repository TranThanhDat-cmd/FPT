using Infrastructure.Utilities;

namespace Infrastructure.Modules.ContentSessions.Requests;

public class GetContentSessionRequest  : PaginationRequest
{
    public Guid? SessionId { get; set; }
}