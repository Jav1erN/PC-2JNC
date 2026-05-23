using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Enums;

namespace Ticketing.Infrastructure.Persistence.Configurations;

public sealed class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("tickets");
        builder.HasKey(ticket => ticket.TicketId);

        builder.Property(ticket => ticket.TicketId).HasColumnName("ticket_id");
        builder.Property(ticket => ticket.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(ticket => ticket.Title).HasColumnName("title").HasMaxLength(255).IsRequired();
        builder.Property(ticket => ticket.Description).HasColumnName("description");
        builder.Property(ticket => ticket.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()");
        builder.Property(ticket => ticket.ClosedAt).HasColumnName("closed_at");
        builder.Property(ticket => ticket.Status)
            .HasColumnName("status")
            .HasMaxLength(20)
            .HasConversion(
                status => ToDatabaseValue(status),
                value => FromDatabaseValue(value))
            .IsRequired();

        builder.ToTable(table => table.HasCheckConstraint("ck_tickets_status", "status IN ('abierto', 'en_proceso', 'cerrado')"));

        builder.HasOne(ticket => ticket.User)
            .WithMany(nameof(User.Tickets))
            .HasForeignKey(ticket => ticket.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(ticket => ticket.Responses).UsePropertyAccessMode(PropertyAccessMode.Field);
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
