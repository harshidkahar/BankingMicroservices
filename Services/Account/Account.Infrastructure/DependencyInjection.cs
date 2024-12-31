using Account.Application.Interface;
using Account.Infrastructure.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Messaging;
using Utility.UnitOfWork;

namespace Account.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database connection
        services.AddScoped<IDbConnection>(sp => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

        // Unit of Work
        //services.AddScoped<IUnitOfWork, Account.Infrastructure.UnitOfWork.UnitOfWork>();
        services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(configuration.GetConnectionString("DefaultConnection")));


        // Account Services
        services.AddScoped<IAccountRepository, AccountRepository>();
        
        // Azure Service Bus
        services.AddSingleton<IEventPublisher, AzureServiceBusPublisher>();
        services.AddSingleton<IEventSubscriber, AzureServiceBusSubscriber>();

        // Logging
        //services.AddLogging(loggingBuilder =>
        //{
        //    loggingBuilder.AddSerilog();
        //});

        return services;
    }
}
