using Core.Bases;
using Infrastructure.Modules.Items.Enums;
using Infrastructure.Modules.Sessions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Modules.Items.Entities;

public class Item : BaseEntity
{
    public string? Name { get; set; }
    public int? Order { get; set; }
    public ItemType? ItemType { get; set; }
    public RoundType? RoundType { get; set; }
    public Guid? SessionId { get; set; }
    public Session Session { get; set; }
}

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Order).IsRequired();
        builder.Property(x => x.ItemType).IsRequired();
        builder.Property(x => x.RoundType).IsRequired();
        builder.HasOne(x => x.Session).WithMany(x => x.Items).HasForeignKey(x => x.SessionId);

    }
}