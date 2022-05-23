using AutoMapper;
using Core.Common.Interfaces;
using Infrastructure.Definitions;
using Infrastructure.Modules.Items.Entities;
using Infrastructure.Modules.Items.Requests;
using Infrastructure.Modules.Sessions.Entities;
using Infrastructure.Modules.Sessions.Requests;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Modules.Items.Servises;

public interface IItemService : IScopedService
{
    Task<(List<Item>? items, string? ErrorMessage)> CreateRangeAsync(Guid sessionId, List<CreateItemRequest> request);
    Task<string?> DeleteRangeAsync(List<Guid> itemIds);
    Task UpdateAsync(Item item, UpdateItemRequest request);
    Task<PaginationResponse<Item>> GetAllAsync(PaginationRequest request);
    Task<(Item? Item, string? ErrorMessage)> GetByIdAsync(Guid id);
    Task<(Item? Item, string? ErrorMessage)> GetDetailAsync(Guid id);
}

public class ItemService : IItemService
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;
    public ItemService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
    }
    public async Task<Item> CreateAsync(CreateItemRequest request)
    {
        Item? item = _mapper.Map<Item>(request);
        await _repositoryWrapper.Items.AddAsync(item);
        return item;
    }

    public async Task<(List<Item>? items, string? ErrorMessage)> CreateRangeAsync(Guid sessionId, List<CreateItemRequest> request)
    {

        Session? session = await _repositoryWrapper.Sessions.GetByIdAsync(sessionId);
        if (session is null) return (null, Messages.Items.SessionIdNotFound);
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

    public async Task<string?> DeleteRangeAsync(List<Guid> itemIds)
    {
        List<Item> items = new List<Item>();
        foreach (Guid itemId in itemIds)
        {
            Item? item = await _repositoryWrapper.Items.GetByIdAsync(itemId);
            if (item is null) return Messages.Items.IdNotFound;
            items.Add(item);
        }
        await _repositoryWrapper.Items.DeleteRangeAsync(items);
        return null;
    }


    public async Task UpdateAsync(Item item, UpdateItemRequest request)
    {
        _mapper.Map(request, item);
        await _repositoryWrapper.Items.UpdateAsync(item);
    }

    public async Task<PaginationResponse<Item>> GetAllAsync(PaginationRequest request)
    {
        IQueryable<Item> items = _repositoryWrapper.Items.Find(x => string.IsNullOrWhiteSpace(request.SearchContent)
                                                                    || x.Name!.ToLower().Contains(request.SearchContent.ToLower()));
        items = SortUtility<Item>.ApplySort(items, request.OrderByQuery);
        PaginationUtility<Item> data = await PaginationUtility<Item>.ToPagedListAsync(items, request.Current, request.PageSize);
        return PaginationResponse<Item>.PaginationInfo(data, data.PageInfo);

    }

    public async Task<(Item? Item, string? ErrorMessage)> GetByIdAsync(Guid id)
    {
        Item? item = await _repositoryWrapper.Items.GetByIdAsync(id);
        if (item is null) return (null, Messages.Items.IdNotFound);
        return (item, null);
    }

    public async Task<(Item? Item, string? ErrorMessage)> GetDetailAsync(Guid id)
    {
        Item? item = await _repositoryWrapper.Items.Find(x => x.Id == id)
            .Include(x => x.Session).FirstOrDefaultAsync();
        if (item is null) return (null, Messages.Items.IdNotFound);
        return (item, null);
    }
}