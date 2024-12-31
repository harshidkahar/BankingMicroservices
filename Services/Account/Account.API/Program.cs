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

var app = builder.Build();

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