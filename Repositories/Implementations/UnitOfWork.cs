using PC_2JNC.Models;
using PC_2JNC.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public Ejercicio1Repository Ej1 { get; }
    public Ejercicio2Repository Ej2 { get; }
    public Ejercicio3Repository Ej3 { get; }
    public Ejercicio4Repository Ej4 { get; }
    public Ejercicio5Repository Ej5 { get; }
    public Ejercicio6Repository Ej6 { get; }
    public Ejercicio7Repository Ej7 { get; }
    public Ejercicio8Repository Ej8 { get; }
    public Ejercicio9Repository Ej9 { get; }
    public Ejercicio10Repository Ej10 { get; }
    public Ejercicio11Repository Ej11 { get; }
    public Ejercicio12Repository Ej12 { get; }

    public UnitOfWork(LabDbContext context)
    {
        Ej1 = new Ejercicio1Repository(context);
        Ej2 = new Ejercicio2Repository(context);
        Ej3 = new Ejercicio3Repository(context);
        Ej4 = new Ejercicio4Repository(context);
        Ej5 = new Ejercicio5Repository(context);
        Ej6 = new Ejercicio6Repository(context);
        Ej7 = new Ejercicio7Repository(context);
        Ej8 = new Ejercicio8Repository(context);
        Ej9 = new Ejercicio9Repository(context);
        Ej10 = new Ejercicio10Repository(context);
        Ej11 = new Ejercicio11Repository(context);
        Ej12 = new Ejercicio12Repository(context);
    }
}
