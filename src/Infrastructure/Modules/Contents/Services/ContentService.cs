using AutoMapper;
using Core.Common.Interfaces;
using Infrastructure.Definitions;
using Infrastructure.Modules.Contents.Entities;
using Infrastructure.Modules.Contents.Requests;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Utilities;

namespace Infrastructure.Modules.Contents.Services;

public interface IContentService : IScopedService
{
    Task<Content> CreateAsync(CreateContentRequest request);
    Task<string?> DeleteRangeAsync(List<Guid> contentIds);
    Task<(Content? Content, string? ErrorMessage)> UpdateAsync(UpdateContentRequest request);
    Task<PaginationResponse<Content>> GetAllAsync(GetContentRequest request);
    Task<(Content? Content, string? ErrorMessage)> GetByIdAsync(Guid id);

}

public class ContentService : IContentService
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;
    public ContentService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
    }


    public async Task<Content> CreateAsync(CreateContentRequest request)
    {
        Content? content = _mapper.Map<Content>(request);
        await _repositoryWrapper.Contents.AddAsync(content);
        return content;
    }

    public async Task<string?> DeleteRangeAsync(List<Guid> contentIds)
    {
        List<Content> contents = new List<Content>();
        foreach (Guid contentId in contentIds)
        {
            var content = await _repositoryWrapper.Contents.GetByIdAsync(contentId);
            if (content is null) return Messages.Contents.IdNotFound;
            contents.Add(content);
        }
        await _repositoryWrapper.Contents.DeleteRangeAsync(contents);
        return null;
    }

    public async Task<(Content? Content, string? ErrorMessage)> UpdateAsync(UpdateContentRequest request)
    {
        Content? content = await _repositoryWrapper.Contents.GetByIdAsync(request.Id);
        if (content is null) return (null, Messages.Contents.IdNotFound);
        _mapper.Map(request, content);
        await _repositoryWrapper.Contents.UpdateAsync(content);
        return (content, null);
    }

    public async Task<PaginationResponse<Content>> GetAllAsync(GetContentRequest request)
    {
        IQueryable<Content> contents = _repositoryWrapper.Contents.Find(x => (request.SearchContent == null
                                        || x.Suggest!.ToLower().Contains(request.SearchContent.ToLower()))
                                        && (request.ContentType == null || x.ContentType == request.ContentType));
        contents = SortUtility<Content>.ApplySort(contents, request.OrderByQuery);
        PaginationUtility<Content> data = await PaginationUtility<Content>.ToPagedListAsync(contents, request.Current, request.PageSize);
        return PaginationResponse<Content>.PaginationInfo(data, data.PageInfo);
    }

    public async Task<(Content? Content, string? ErrorMessage)> GetByIdAsync(Guid id)
    {
        Content? content = await _repositoryWrapper.Contents.GetByIdAsync(id);
        if (content is null) return (null, Messages.Contents.IdNotFound);
        return (content, null);
    }



}