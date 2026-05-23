using Microsoft.Extensions.DependencyInjection;
using Ticketing.Application.UseCases.Auth;
using Ticketing.Application.UseCases.Roles;
using Ticketing.Application.UseCases.Tickets;

namespace Ticketing.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RegisterUserUseCase>();
        services.AddScoped<LoginUserUseCase>();
        services.AddScoped<CreateTicketUseCase>();
        services.AddScoped<GetAllTicketsUseCase>();
        services.AddScoped<GetTicketByIdUseCase>();
        services.AddScoped<CloseTicketUseCase>();
        services.AddScoped<AddResponseToTicketUseCase>();
        services.AddScoped<AssignRoleToUserUseCase>();
        services.AddScoped<GetAllRolesUseCase>();

        return services;
    }
}
