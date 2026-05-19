namespace PC_2JNC.Aplicacion.DTOs.Reportes;

public sealed record VentasAgrupadasPorClienteDto(
    int ClienteId,
    string NombreCliente,
    int TotalPedidos,
    int TotalUnidades,
    decimal TotalVentas);
