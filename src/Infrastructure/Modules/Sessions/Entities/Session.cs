using Core.Bases;
using Infrastructure.Modules.Items.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Modules.Sessions.Entities;

public class Session : BaseEntity
{
    public string? Name { get; set; }
    public List<Item> Items { get; set; }
    public List<ContentSession> ContentSessions { get; set; }
}
public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
    }
}
