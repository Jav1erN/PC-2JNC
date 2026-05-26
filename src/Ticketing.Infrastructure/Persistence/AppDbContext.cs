using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Enums;

namespace Ticketing.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<User> Users => Set<User>();

    public DbSet<UserRole> UserRoles => Set<UserRole>();

    public DbSet<Ticket> Tickets => Set<Ticket>();

    public DbSet<Response> Responses => Set<Response>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Response>(entity =>
        {
            entity.ToTable("responses");

            entity.HasKey(response => response.ResponseId);
            entity.HasIndex(response => response.ResponderId, "IX_responses_responder_id");
            entity.HasIndex(response => response.TicketId, "IX_responses_ticket_id");

            entity.Property(response => response.ResponseId)
                .ValueGeneratedNever()
                .HasColumnName("response_id");
            entity.Property(response => response.TicketId).HasColumnName("ticket_id").IsRequired();
            entity.Property(response => response.ResponderId).HasColumnName("responder_id").IsRequired();
            entity.Property(response => response.Message).HasColumnName("message").IsRequired();
            entity.Property(response => response.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");

            entity.HasOne(response => response.Ticket)
                .WithMany(nameof(Ticket.Responses))
                .HasForeignKey(response => response.TicketId);

            entity.HasOne(response => response.Responder)
                .WithMany(nameof(User.Responses))
                .HasForeignKey(response => response.ResponderId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("roles");

            entity.HasKey(role => role.RoleId);
            entity.HasIndex(role => role.RoleName, "IX_roles_role_name").IsUnique();

            entity.Property(role => role.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("role_id");
            entity.Property(role => role.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name")
                .IsRequired();

            entity.Navigation(role => role.UserRoles).UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("tickets");

            entity.HasKey(ticket => ticket.TicketId);
            entity.HasIndex(ticket => ticket.UserId, "IX_tickets_user_id");

            entity.Property(ticket => ticket.TicketId)
                .ValueGeneratedNever()
                .HasColumnName("ticket_id");
            entity.Property(ticket => ticket.UserId).HasColumnName("user_id").IsRequired();
            entity.Property(ticket => ticket.Title)
                .HasMaxLength(255)
                .HasColumnName("title")
                .IsRequired();
            entity.Property(ticket => ticket.Description).HasColumnName("description");
            entity.Property(ticket => ticket.Status)
                .HasMaxLength(20)
                .HasColumnName("status")
                .HasConversion(
                    status => ToDatabaseValue(status),
                    value => FromDatabaseValue(value))
                .IsRequired();
            entity.Property(ticket => ticket.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(ticket => ticket.ClosedAt).HasColumnName("closed_at");

            entity.HasOne(ticket => ticket.User)
                .WithMany(nameof(User.Tickets))
                .HasForeignKey(ticket => ticket.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Navigation(ticket => ticket.Responses).UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.HasKey(user => user.UserId);
            entity.HasIndex(user => user.Email, "IX_users_email").IsUnique();
            entity.HasIndex(user => user.Username, "IX_users_username").IsUnique();

            entity.Property(user => user.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(user => user.Username)
                .HasMaxLength(100)
                .HasColumnName("username")
                .IsRequired();
            entity.Property(user => user.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash")
                .IsRequired();
            entity.Property(user => user.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(user => user.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");

            entity.Navigation(user => user.UserRoles).UsePropertyAccessMode(PropertyAccessMode.Field);
            entity.Navigation(user => user.Tickets).UsePropertyAccessMode(PropertyAccessMode.Field);
            entity.Navigation(user => user.Responses).UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("user_roles");

            entity.HasKey(userRole => new { userRole.UserId, userRole.RoleId });
            entity.HasIndex(userRole => userRole.RoleId, "IX_user_roles_role_id");

            entity.Property(userRole => userRole.UserId).HasColumnName("user_id");
            entity.Property(userRole => userRole.RoleId).HasColumnName("role_id");
            entity.Property(userRole => userRole.AssignedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("assigned_at");

            entity.HasOne(userRole => userRole.Role)
                .WithMany(nameof(Role.UserRoles))
                .HasForeignKey(userRole => userRole.RoleId);

            entity.HasOne(userRole => userRole.User)
                .WithMany(nameof(User.UserRoles))
                .HasForeignKey(userRole => userRole.UserId);
        });
    }

    private static string ToDatabaseValue(TicketStatus status)
    {
        return status switch
        {
            TicketStatus.Open => "abierto",
            TicketStatus.InProgress => "en_proceso",
            TicketStatus.Closed => "cerrado",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Unsupported ticket status.")
        };
    }

    private static TicketStatus FromDatabaseValue(string status)
    {
        return status switch
        {
            "abierto" => TicketStatus.Open,
            "en_proceso" => TicketStatus.InProgress,
            "cerrado" => TicketStatus.Closed,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Unsupported ticket status.")
        };
    }
}
