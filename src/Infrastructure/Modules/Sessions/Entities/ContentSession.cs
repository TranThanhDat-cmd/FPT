using Core.Bases;
using Infrastructure.Modules.Contents.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Modules.Sessions.Entities;

public class ContentSession
{
    public ContentSession()
    {
        CreatedAt = DateTime.UtcNow;
    }
    public Guid ContentId { get; set; }
    public Content Content { get; set; }
    public Guid SessionId { get; set; }
    public Session Session { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class ContentSessionConfiguration : IEntityTypeConfiguration<ContentSession>
{
    public void Configure(EntityTypeBuilder<ContentSession> builder)
    {
        builder.HasKey(x => new { x.SessionId, x.ContentId });
        builder.Property(x => x.ContentId).IsRequired();
        builder.Property(x => x.SessionId).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.HasOne(x => x.Content).WithMany(x => x.ContentSessions).HasForeignKey(x => x.ContentId);
        builder.HasOne(x => x.Session).WithMany(x => x.ContentSessions).HasForeignKey(x => x.SessionId);

    }
}
