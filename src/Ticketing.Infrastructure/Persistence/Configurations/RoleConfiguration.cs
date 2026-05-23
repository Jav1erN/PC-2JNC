using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities;

namespace Ticketing.Infrastructure.Persistence.Configurations;

public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        builder.HasKey(role => role.RoleId);

        builder.Property(role => role.RoleId).HasColumnName("role_id");
        builder.Property(role => role.RoleName).HasColumnName("role_name").HasMaxLength(50).IsRequired();

        builder.HasIndex(role => role.RoleName).IsUnique();
        builder.Navigation(role => role.UserRoles).UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasData(
            CreateSeedRole("11111111-1111-1111-1111-111111111111", "Admin"),
            CreateSeedRole("22222222-2222-2222-2222-222222222222", "Support"),
            CreateSeedRole("33333333-3333-3333-3333-333333333333", "Client"));
    }

    private static Role CreateSeedRole(string id, string name)
    {
        var role = Role.Create(name);
        typeof(Role).GetProperty(nameof(Role.RoleId))!.SetValue(role, Guid.Parse(id));
        return role;
    }
}
