using PC_2JNC.Aplicacion.DTOs.Pedidos;

namespace PC_2JNC.Aplicacion.DTOs.Clientes;

public sealed record ClienteConPedidosDto(
    int ClienteId,
    string Nombre,
    string Email,
    IReadOnlyCollection<PedidoResumenDto> Pedidos);
