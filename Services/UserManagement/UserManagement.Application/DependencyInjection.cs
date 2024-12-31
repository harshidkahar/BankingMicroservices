
using UserManagement.Application.Authentication.Commands.Login;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace UserManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddMediatR(typeof(RegisterCommandHandler).Assembly);
        return services;
    }
}
