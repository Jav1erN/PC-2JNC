using Microsoft.EntityFrameworkCore;
using PC_2JNC.Aplicacion.CasosDeUso.Clientes;
using PC_2JNC.Aplicacion.CasosDeUso.Pedidos;
using PC_2JNC.Aplicacion.CasosDeUso.Productos;
using PC_2JNC.Aplicacion.CasosDeUso.Reportes;
using PC_2JNC.Aplicacion.Interfaces;
using PC_2JNC.Infraestructura.Data.DbContext;
using PC_2JNC.Infraestructura.Data.Repositories;
using PC_2JNC.Infraestructura.Data.UnitOfWork;

namespace PC_2JNC.API.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationUseCases(this IServiceCollection services)
    {
        services.AddScoped<IClientesUseCase, ClientesUseCase>();
        services.AddScoped<IProductosUseCase, ProductosUseCase>();
        services.AddScoped<IPedidosUseCase, PedidosUseCase>();
        services.AddScoped<IReportesUseCase, ReportesUseCase>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<LabDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IProductoRepository, ProductoRepository>();
        services.AddScoped<IPedidoRepository, PedidoRepository>();
        services.AddScoped<IReporteVentasRepository, ReporteVentasRepository>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        return services;
    }
}
