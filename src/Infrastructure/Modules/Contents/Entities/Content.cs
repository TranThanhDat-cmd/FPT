using Core.Bases;
using Infrastructure.Modules.Contents.Enums;
using Infrastructure.Modules.Items.Enums;
using Infrastructure.Modules.Sessions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Modules.Contents.Entities;

public class Content : BaseEntity
{
    public ContentType? ContentType { get; set; }
    public string? Suggest { get; set; }
    public List<ContentSession>? ContentSessions { get; set; }
}

public class ContentConfiguration : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ContentType).IsRequired();
        builder.Property(x => x.Suggest).IsRequired();

    }
}