namespace PC_2JNC.Infraestructura.LINQ;

public static class QueryableExtensions
{
    public static IQueryable<T> Paginar<T>(this IQueryable<T> query, int pagina, int tamanoPagina)
    {
        var paginaNormalizada = pagina < 1 ? 1 : pagina;
        var tamanoNormalizado = tamanoPagina switch
        {
            < 1 => 20,
            > 100 => 100,
            _ => tamanoPagina
        };

        return query.Skip((paginaNormalizada - 1) * tamanoNormalizado).Take(tamanoNormalizado);
    }
}
