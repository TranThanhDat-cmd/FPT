using Core.Common.Interfaces;
using Infrastructure.Modules.Contents.Entities;
using Infrastructure.Modules.Items.Entities;
using Infrastructure.Modules.Sessions.Entities;
using Infrastructure.Modules.Users.Entities;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Persistence.Repositories;

public interface IRepositoryWrapper : IScopedService
{
    IRepositoryBase<User> Users { get; }
    
    IRepositoryBase<Session> Sessions { get; }
    IRepositoryBase<Item> Items { get; }
    IRepositoryBase<Content> Contents { get; }
    IRepositoryBase<ContentSession> ContentSessions { get; }
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}

public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly ApplicationDbContext ApplicationDbContext;

    public RepositoryWrapper(ApplicationDbContext applicationDbContext) => ApplicationDbContext = applicationDbContext;

    private IRepositoryBase<User>? UsersRepositoryBase;
    public IRepositoryBase<User> Users => UsersRepositoryBase ??= new RepositoryBase<User>(ApplicationDbContext);
    
    private IRepositoryBase<Content>? ContentRepositoryBase;
    public IRepositoryBase<Content> Contents => ContentRepositoryBase ??= new RepositoryBase<Content>(ApplicationDbContext);
    
    private IRepositoryBase<ContentSession>? ContentSessionRepositoryBase;
    public IRepositoryBase<ContentSession> ContentSessions => ContentSessionRepositoryBase ??= new RepositoryBase<ContentSession>(ApplicationDbContext);
    
    private IRepositoryBase<Item>? ItemsRepositoryBase;
    public IRepositoryBase<Item> Items => ItemsRepositoryBase ??= new RepositoryBase<Item>(ApplicationDbContext);
    private IRepositoryBase<Session>? SessionRepositoryBase;
    public IRepositoryBase<Session> Sessions => SessionRepositoryBase ??= new RepositoryBase<Session>(ApplicationDbContext);
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default) => await ApplicationDbContext.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default) => await ApplicationDbContext.Database.CommitTransactionAsync(cancellationToken);

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default) => await ApplicationDbContext.Database.RollbackTransactionAsync(cancellationToken);
}
