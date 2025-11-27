using BancoApi.Api.Data;
using BancoApi.Api.GraphQL;
using BancoApi.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configure EF Core - Use PostgreSQL if connection string is provided, otherwise InMemory
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (!string.IsNullOrEmpty(connectionString) && connectionString.Contains("Host="))
{
    builder.Services.AddPooledDbContextFactory<BankDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    builder.Services.AddPooledDbContextFactory<BankDbContext>(options =>
        options.UseInMemoryDatabase("BancoDb"));
}

builder.Services.AddScoped<ContaService>();

// Add GraphQL (HotChocolate)
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

// Ensure DB is created and seeded
using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<BankDbContext>>();
    using var db = factory.CreateDbContext();
    db.Database.EnsureCreated();
}

app.MapGraphQL();

app.Run();
