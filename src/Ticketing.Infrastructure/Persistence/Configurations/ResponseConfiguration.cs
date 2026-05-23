using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ticketing.Domain.Entities;

namespace Ticketing.Infrastructure.Persistence.Configurations;

public sealed class ResponseConfiguration : IEntityTypeConfiguration<Response>
{
    public void Configure(EntityTypeBuilder<Response> builder)
    {
        builder.ToTable("responses");
        builder.HasKey(response => response.ResponseId);

        builder.Property(response => response.ResponseId).HasColumnName("response_id");
        builder.Property(response => response.TicketId).HasColumnName("ticket_id").IsRequired();
        builder.Property(response => response.ResponderId).HasColumnName("responder_id").IsRequired();
        builder.Property(response => response.Message).HasColumnName("message").IsRequired();
        builder.Property(response => response.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()");

        builder.HasOne(response => response.Ticket)
            .WithMany(nameof(Ticket.Responses))
            .HasForeignKey(response => response.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(response => response.Responder)
            .WithMany(nameof(User.Responses))
            .HasForeignKey(response => response.ResponderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
