using AutoMapper;
using Core.Common.Interfaces;
using Infrastructure.Definitions;
using Infrastructure.Modules.Contents.Entities;
using Infrastructure.Modules.Contents.Enums;
using Infrastructure.Modules.Items.Entities;
using Infrastructure.Modules.Sessions.Entities;
using Infrastructure.Modules.Sessions.Requests;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Modules.Sessions.Services;

public interface ISessionService : IScopedService
{
    Task<Session> CreateAsync(CreateSessionRequest request);
    Task<string?> DeleteRangeAsync(List<Guid> sessionIds);
    Task UpdateAsync(Session question, UpdateSessionRequest request);
    Task<PaginationResponse<Session>> GetAllAsync(PaginationRequest request);
    Task<(Session? Session, string? ErrorMessage)> GetByIdAsync(Guid id);
    Task<(Session? Session, string? ErrorMessage)> GetDetailAsync(Guid id);
    Task<(List<ContentSession>? ContentSessions, string? ErrorMessage)> CreateRangeContentSessionAsync(Guid sessionId, List<CreateContentSessionRequest> request);
    Task<string?> DeleteRangeContentSessionAsync(Guid sessionId, DeleteContentSessionRequest request);
    Task<PaginationResponse<Content>> GetContentBySessionId(Guid sessionId, GetContentBySessionIdRequest request);
    Task<PaginationResponse<Item>> GetItemBySessionId(Guid sessionId, GetItemBySessionIdRequest request);
    Task<(List<Item>? items, string? ErrorMessage)> CreateRangeItemAsync(Guid sessionId, List<CreateItemRequest> request);
    Task DeleteRangeItemAsync(Guid sessionId, DeleteItemsRequest request);
    Task<(List<Item>? items, string? ErrorMessage)> UpdateRangeItemAsync(Guid sessionId, List<UpdateItemsRequest> request);
    Task<(List<ContentSession>? ContentSessions, string? ErrorMessage)> UpdateRangeContentSessionAsync(Guid sessionId, ContentType contentType, List<UpdateContentSessionRequest> request);

}

public class SessionService : ISessionService
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;
    public SessionService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
    }

    public async Task<(List<Item>? items, string? ErrorMessage)> CreateRangeItemAsync(Guid sessionId, List<CreateItemRequest> request)
    {

        Session? session = await _repositoryWrapper.Sessions.GetByIdAsync(sessionId);
        if (session is null) return (null, Messages.Items.SessionIdNotFound);
        List<Item>? itemsExisting = await _repositoryWrapper.Items
           .Find(x => x.SessionId == sessionId && x.RoundType == request.ElementAt(0).RoundType)
           .ToListAsync();
        if (itemsExisting.Count() != 0) return (null, Messages.Items.ItemIsExisting);
        List<Item> items = _mapper.Map<List<Item>>(request);
        items = items.Select(x =>
        {
            x.SessionId = sessionId;
            return x;
        }).ToList();
        await _repositoryWrapper.Items.AddRangeAsync(items);
        items = items.Select(x =>
        {
            x.Session = null;
            return x;
        }).ToList();
        return (items, null);
    }
    public async Task<Session> CreateAsync(CreateSessionRequest request)
    {
        Session? session = _mapper.Map<Session>(request);
        await _repositoryWrapper.Sessions.AddAsync(session);
        return session;
    }

    public async Task<string?> DeleteRangeAsync(List<Guid> sessionIds)
    {
        List<Session> sessions = new List<Session>();
        foreach (Guid sessionId in sessionIds)
        {
            Session? session = await _repositoryWrapper.Sessions.GetByIdAsync(sessionId);
            if (session is null) return Messages.Sessions.IdNotFound;
            sessions.Add(session);
        }
        await _repositoryWrapper.Sessions.DeleteRangeAsync(sessions);
        return null;
    }


    public async Task UpdateAsync(Session session, UpdateSessionRequest request)
    {
        _mapper.Map(request, session);
        await _repositoryWrapper.Sessions.UpdateAsync(session);
    }

    public async Task<PaginationResponse<Session>> GetAllAsync(PaginationRequest request)
    {
        IQueryable<Session> sessions = _repositoryWrapper.Sessions
            .Find(x => string.IsNullOrEmpty(request.SearchContent) || x.Name!.ToLower().Contains(request.SearchContent.ToLower()));
        sessions = SortUtility<Session>.ApplySort(sessions, request.OrderByQuery);
        PaginationUtility<Session> data = await PaginationUtility<Session>
            .ToPagedListAsync(sessions, request.Current, request.PageSize);
        return PaginationResponse<Session>.PaginationInfo(data, data.PageInfo);
    }

    public async Task<(Session? Session, string? ErrorMessage)> GetByIdAsync(Guid id)
    {
        Session? session = await _repositoryWrapper.Sessions.GetByIdAsync(id);
        if (session is null) return (null, Messages.Sessions.IdNotFound);
        return (session, null);
    }

    public async Task<(Session? Session, string? ErrorMessage)> GetDetailAsync(Guid id)
    {
        Session? session = await _repositoryWrapper.Sessions.Find(x => x.Id == id)
            .Include(x => x.Items)
            .Include(x => x.ContentSessions)
            .FirstOrDefaultAsync();
        if (session is null) return (null, Messages.Sessions.IdNotFound);
        return (session, null);
    }

    public async Task<(List<ContentSession>? ContentSessions, string? ErrorMessage)> CreateRangeContentSessionAsync
        (Guid sessionId, List<CreateContentSessionRequest> request)
    {
        Session? session = await _repositoryWrapper.Sessions.GetByIdAsync(sessionId);
        if (session is null) return (null, Messages.Sessions.IdNotFound);
        List<ContentSession>? contentSessions = _mapper.Map<List<ContentSession>>(request);
        contentSessions = contentSessions.Select(x =>
        {
            x.SessionId = sessionId;
            return x;
        }).ToList();

        await _repositoryWrapper.ContentSessions.AddRangeAsync(contentSessions);
        return (contentSessions, null);
    }

    public async Task<string?> DeleteRangeContentSessionAsync(Guid sessionId, DeleteContentSessionRequest request)
    {
        IQueryable<ContentSession> contentSessions = _repositoryWrapper.ContentSessions
            .Find(x => x.SessionId == sessionId && request.ContentIds.Contains(x.ContentId));
        if (contentSessions.Count() != request.ContentIds!.Count) return (Messages.Contents.IdNotFound);
        await _repositoryWrapper.ContentSessions.DeleteRangeAsync(contentSessions);
        return null;
    }

    public async Task<PaginationResponse<Content>> GetContentBySessionId(Guid sessionId, GetContentBySessionIdRequest request)
    {
        IQueryable<Content> contents = _repositoryWrapper.ContentSessions
            .Find(x => x.SessionId == sessionId
                       && (request.ContentType == null || request.ContentType == x.Content.ContentType))
            .Include(x => x.Content)
            .Select(x => x.Content);

        PaginationUtility<Content> data = await PaginationUtility<Content>.ToPagedListAsync(contents, request.Current, request.PageSize);
        return PaginationResponse<Content>.PaginationInfo(data, data.PageInfo);
    }

    public async Task<PaginationResponse<Item>> GetItemBySessionId(Guid sessionId, GetItemBySessionIdRequest request)
    {
        IQueryable<Item> items = _repositoryWrapper.Items
           .Find(x => x.SessionId == sessionId
           && (request.ItemType == null || x.ItemType == request.ItemType)
           && (request.RoundType == null || x.RoundType == request.RoundType))
           .OrderBy(x => x.Order);
        PaginationUtility<Item> data = await PaginationUtility<Item>.ToPagedListAsync(items, request.Current, request.PageSize);
        return PaginationResponse<Item>.PaginationInfo(data, data.PageInfo);
    }

    public async Task DeleteRangeItemAsync(Guid sessionId, DeleteItemsRequest request)
    {
        IQueryable<Item> items = _repositoryWrapper.Items
            .Find(x => x.SessionId == sessionId && request.ItemIds.Contains(x.Id));
        await _repositoryWrapper.Items.DeleteRangeAsync(items);
    }

    public async Task<(List<Item>? items, string? ErrorMessage)> UpdateRangeItemAsync(Guid sessionId, List<UpdateItemsRequest> request)
    {
        Session? session = await _repositoryWrapper.Sessions.GetByIdAsync(sessionId);
        if (session is null) return (null, Messages.Sessions.IdNotFound);
        List<Item>? items = await _repositoryWrapper.Items
            .Find(x => x.SessionId == sessionId && x.RoundType == request.ElementAt(0).RoundType)
            .ToListAsync();
        await _repositoryWrapper.Items.DeleteRangeAsync(items);

        items = _mapper.Map<List<Item>>(request);
        items = items.Select(x =>
        {
            x.SessionId = sessionId;
            return x;
        }).ToList();
        await _repositoryWrapper.Items.AddRangeAsync(items);
        return (items, null);
    }

    public async Task<(List<ContentSession>? ContentSessions, string? ErrorMessage)> UpdateRangeContentSessionAsync
        (Guid sessionId, ContentType contentType, List<UpdateContentSessionRequest> request)
    {
        Session? session = await _repositoryWrapper.Sessions.GetByIdAsync(sessionId);
        if (session is null) return (null, Messages.Sessions.IdNotFound);
        IQueryable<ContentSession> contentSessions = _repositoryWrapper.ContentSessions
            .Find(x => x.SessionId == sessionId && x.Content.ContentType == contentType);
        await _repositoryWrapper.ContentSessions.DeleteRangeAsync(contentSessions);
        List<ContentSession>? newContentSessions = _mapper.Map<List<ContentSession>>(request);
        newContentSessions = newContentSessions.Select(x =>
        {
            x.SessionId = sessionId;
            return x;
        }).ToList();
        await _repositoryWrapper.ContentSessions.AddRangeAsync(newContentSessions);
        return (newContentSessions, null);
    }
}