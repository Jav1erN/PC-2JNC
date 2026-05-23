using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities;

namespace Ticketing.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(user => user.UserId);

        builder.Property(user => user.UserId).HasColumnName("user_id");
        builder.Property(user => user.Username).HasColumnName("username").HasMaxLength(100).IsRequired();
        builder.Property(user => user.PasswordHash).HasColumnName("password_hash").HasMaxLength(255).IsRequired();
        builder.Property(user => user.Email).HasColumnName("email").HasMaxLength(150);
        builder.Property(user => user.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()");

        builder.HasIndex(user => user.Username).IsUnique();
        builder.HasIndex(user => user.Email).IsUnique();

        builder.Navigation(user => user.UserRoles).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(user => user.Tickets).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(user => user.Responses).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
