namespace PC_2JNC.Aplicacion.DTOs.Reportes;

public sealed record ResumenComprasClienteDto(
    int ClienteId,
    string NombreCliente,
    int CantidadPedidos,
    int CantidadProductos,
    decimal TotalComprado);
