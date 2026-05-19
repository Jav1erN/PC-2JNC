namespace PC_2JNC.Aplicacion.DTOs.Comun;

public sealed record ResultadoPaginadoDto<T>(
    IReadOnlyCollection<T> Items,
    int TotalRegistros,
    int Pagina,
    int TamanoPagina)
{
    public int TotalPaginas => TamanoPagina <= 0
        ? 0
        : (int)Math.Ceiling((double)TotalRegistros / TamanoPagina);
}
