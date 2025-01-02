using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using MediatR;
using Account.Application;
using Account.Infrastructure;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Account.Api.Common.Errors;
using Account.Application.AccountManagement.Commands.CreateAccount;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container (ConfigureServices logic)
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers()
     .AddFluentValidation(fv =>
     {
         fv.RegisterValidatorsFromAssemblyContaining<CreateAccountCommandValidator>();
         fv.AutomaticValidationEnabled = true;
     });
builder.Services.AddSingleton<ProblemDetailsFactory, AccountProblemDetailsFactory>();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Account Microservices",
        Description = "This project is for handling account related microservices",
        Contact = new OpenApiContact
        {
            Name = "Harshid Kahar",
            Email = "harshid10789@gmail.com",
            Url = new Uri("https://github.com/harshidkahar")
        }
    });

    // Optional: Add JWT authentication in Swagger (if applicable)
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token in the format: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = "YourIssuer",
            ValidAudience = "YourAudience"

        };
    });
builder.Services.AddAuthorization();
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());

// Add MediatR, Dapper, and other services
builder.Services.AddMediatR(typeof(Program).Assembly);
//builder.Services.AddMediatR(typeof(LoginCommandHandler).Assembly); // Specify an assembly where handlers are defined

builder.Services.AddRateLimiter(ratelimiterOptions =>
{
    ratelimiterOptions.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    {
        // Use IP Address as the partition key
        var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        return RateLimitPartition.GetFixedWindowLimiter(clientIp, _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 3, // Allow 100 requests
            Window = TimeSpan.FromSeconds(10), // Per 1 minute
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0 // Allow 10 additional requests in the queue
        });
    });

    // Customize rate limiting response
    ratelimiterOptions.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        await context.HttpContext.Response.WriteAsync("Rate limit exceeded. Please try again later.", cancellationToken);
    };


    ratelimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    ratelimiterOptions.AddFixedWindowLimiter("fixedwindow", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 3;
        options.QueueLimit = 0;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });

    ratelimiterOptions.AddSlidingWindowLimiter("slidingWindow", options =>
    {
        options.Window = TimeSpan.FromSeconds(15);
        options.SegmentsPerWindow = 3;
        options.PermitLimit = 15;
    });

    ratelimiterOptions.AddTokenBucketLimiter("token", options =>
    {
        options.TokenLimit = 100;
        options.ReplenishmentPeriod = TimeSpan.FromSeconds(5);
        options.TokensPerPeriod = 10;
    });

    ratelimiterOptions.AddConcurrencyLimiter("concurrency", options =>
    {
        options.PermitLimit = 5;
    });
});

var app = builder.Build();

app.UseRateLimiter();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo Project API V1");
        options.RoutePrefix = string.Empty; // Set Swagger UI as the app's root
    });
}
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();