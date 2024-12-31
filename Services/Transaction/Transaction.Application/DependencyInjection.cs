
using Transaction.Application.Authentication.Commands.Login;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Application.Authentication.Commands.TransferFunds;

namespace Transaction.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddMediatR(typeof(TransferFundsCommandHandler).Assembly);
        //builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        return services;
    }
}
