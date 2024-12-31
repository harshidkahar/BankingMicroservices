
using Account.Application.AccountManagement.Commands.CreateAccount;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddMediatR(typeof(CreateAccountCommandHandler).Assembly);
        return services;
    }
}
