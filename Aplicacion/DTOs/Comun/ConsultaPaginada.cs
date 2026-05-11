namespace PC_2JNC.Aplicacion.DTOs.Comun;

public sealed record ConsultaPaginada(int Pagina = 1, int TamanoPagina = 20)
{
    public int PaginaNormalizada => Pagina < 1 ? 1 : Pagina;
    public int TamanoPaginaNormalizado => TamanoPagina switch
    {
        < 1 => 20,
        > 100 => 100,
        _ => TamanoPagina
    };

    public int Saltar => (PaginaNormalizada - 1) * TamanoPaginaNormalizado;
}
