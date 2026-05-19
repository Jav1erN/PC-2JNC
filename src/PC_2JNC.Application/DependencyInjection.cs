using Microsoft.Extensions.DependencyInjection;
using PC_2JNC.Aplicacion.CasosDeUso.Clientes;
using PC_2JNC.Aplicacion.CasosDeUso.Pedidos;
using PC_2JNC.Aplicacion.CasosDeUso.Productos;
using PC_2JNC.Aplicacion.CasosDeUso.Reportes;
using PC_2JNC.Application.UseCases.Auth;

namespace PC_2JNC.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthUseCase, AuthUseCase>();
        services.AddScoped<IClientesUseCase, ClientesUseCase>();
        services.AddScoped<IProductosUseCase, ProductosUseCase>();
        services.AddScoped<IPedidosUseCase, PedidosUseCase>();
        services.AddScoped<IReportesUseCase, ReportesUseCase>();

        return services;
    }
}
