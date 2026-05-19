using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PC_2JNC.Domain.Entities;

namespace PC_2JNC.Infraestructura.Data.Configurations;

public sealed class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("orders");
        builder.HasKey(pedido => pedido.PedidoId);

        builder.Property(pedido => pedido.PedidoId).HasColumnName("orderid");
        builder.Property(pedido => pedido.ClienteId).HasColumnName("clientid");
        builder.Property(pedido => pedido.FechaPedido).HasColumnName("orderdate");

        builder.HasMany(pedido => pedido.DetallesPedido)
            .WithOne(detalle => detalle.Pedido)
            .HasForeignKey(detalle => detalle.PedidoId);
    }
}
