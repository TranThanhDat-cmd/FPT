using Core.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;


namespace Infrastructure.Modules.Users.Entities;

public class User : BaseEntity
{
    public string? EmailAddress { get; set; }
    [JsonIgnore]
    public string? PasswordHash { get; set; }
    public string? FullName { get; set; }
    [JsonIgnore]
    public string? Avatar { get; set; }
    [JsonIgnore]
    public string? Code { get; set; }
    [JsonIgnore]
    public DateTime? CodeExpires { get; set; }
}

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.EmailAddress).IsRequired();
        builder.Property(x => x.PasswordHash).IsRequired();
        builder.Property(x => x.FullName).IsRequired();
    }
}
