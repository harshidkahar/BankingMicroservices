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

namespace Utility;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Unit of Work
        services.AddScoped<IUnitOfWork>(provider => new UnitOfWork.UnitOfWork(configuration.GetConnectionString("DefaultConnection")));

        // Logging
        //services.AddLogging(loggingBuilder =>
        //{
        //    loggingBuilder.AddSerilog();
        //});

        return services;
    }
}
