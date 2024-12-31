using Transaction.Application.Interface;
using Transaction.Infrastructure.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Transaction.Application.Authentication.Commands.TransferFunds;
using Utility.UnitOfWork;

namespace Transaction.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database connection
        services.AddScoped<IDbConnection>(sp => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

        // Unit of Work
        //services.AddScoped<IUnitOfWork, Transaction.Infrastructure.UnitOfWork.UnitOfWork>();
        services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(configuration.GetConnectionString("DefaultConnection")));


        // Transaction Service
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddHttpClient<IAccountService, AccountService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7184");
        });
//.AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10)))
//.AddPolicyHandler(Policy.Handle<HttpRequestException>().RetryAsync(3));
        services.AddMediatR(typeof(TransferFundsCommandHandler).Assembly);
        // Logging
        //services.AddLogging(loggingBuilder =>
        //{
        //    loggingBuilder.AddSerilog();
        //});

        return services;
    }
}
