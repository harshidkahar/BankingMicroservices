var builder = DistributedApplication.CreateBuilder(args);


builder.AddProject<Projects.Ocelot_ApiGateway>("ocelot-apigateway");
builder.AddProject<Projects.Account_API>("account-API");
builder.AddProject<Projects.Transaction_API>("transaction-API");
builder.AddProject<Projects.UserManagement_API>("user-management-API");

builder.Build().Run();
