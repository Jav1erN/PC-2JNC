using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PC_2JNC.Domain.Entities;

namespace PC_2JNC.Infraestructura.Data.Configurations;

public sealed class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("clients");
        builder.HasKey(cliente => cliente.ClienteId);

        builder.Property(cliente => cliente.ClienteId).HasColumnName("clientid");
        builder.Property(cliente => cliente.Nombre).HasColumnName("name").HasMaxLength(150).IsRequired();
        builder.Property(cliente => cliente.Email).HasColumnName("email").HasMaxLength(200).IsRequired();

        builder.HasMany(cliente => cliente.Pedidos)
            .WithOne(pedido => pedido.Cliente)
            .HasForeignKey(pedido => pedido.ClienteId);
    }
}
