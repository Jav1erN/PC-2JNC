using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PC_2JNC.Domain.Entities;

namespace PC_2JNC.Infraestructura.Data.Configurations;

public sealed class ProductoConfiguration : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {
        builder.ToTable("products");
        builder.HasKey(producto => producto.ProductoId);

        builder.Property(producto => producto.ProductoId).HasColumnName("productid");
        builder.Property(producto => producto.Nombre).HasColumnName("name").HasMaxLength(150).IsRequired();
        builder.Property(producto => producto.Descripcion).HasColumnName("description").HasMaxLength(500);
        builder.Property(producto => producto.Precio).HasColumnName("price").HasPrecision(18, 2);

        builder.HasMany(producto => producto.DetallesPedido)
            .WithOne(detalle => detalle.Producto)
            .HasForeignKey(detalle => detalle.ProductoId);
    }
}
