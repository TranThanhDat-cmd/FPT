
using Infrastructure.Modules.Contents.Entities;
using Infrastructure.Modules.Items.Entities;
using Infrastructure.Modules.Sessions.Entities;
using Infrastructure.Modules.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users => Set<User>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Content> Contents => Set<Content>();
    public DbSet<ContentSession> ContentSessions => Set<ContentSession>();


    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

}
