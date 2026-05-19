namespace PC_2JNC.Aplicacion.DTOs.Productos;

public sealed record ProductoDto(int ProductoId, string Nombre, string? Descripcion, decimal Precio);
