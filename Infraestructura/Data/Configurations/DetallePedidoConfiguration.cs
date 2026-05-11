using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PC_2JNC.Dominio.Entidades;

namespace PC_2JNC.Infraestructura.Data.Configurations;

public sealed class DetallePedidoConfiguration : IEntityTypeConfiguration<DetallePedido>
{
    public void Configure(EntityTypeBuilder<DetallePedido> builder)
    {
        builder.ToTable("orderdetails");
        builder.HasKey(detalle => detalle.DetallePedidoId);

        builder.Property(detalle => detalle.DetallePedidoId).HasColumnName("orderdetailid");
        builder.Property(detalle => detalle.PedidoId).HasColumnName("orderid");
        builder.Property(detalle => detalle.ProductoId).HasColumnName("productid");
        builder.Property(detalle => detalle.Cantidad).HasColumnName("quantity");
    }
}
