using UserManagement.Application.Interface;
using UserManagement.Infrastructure.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.UnitOfWork;

namespace UserManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database connection
        services.AddScoped<IDbConnection>(sp => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

        // Unit of Work
        //services.AddScoped<IUnitOfWork, UserManagement.Infrastructure.UnitOfWork.UnitOfWork>();
        services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(configuration.GetConnectionString("DefaultConnection")));


        // Authentication Service
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        // JWT Token Generator
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        // Logging
        //services.AddLogging(loggingBuilder =>
        //{
        //    loggingBuilder.AddSerilog();
        //});

        return services;
    }
}
