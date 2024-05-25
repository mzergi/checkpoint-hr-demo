using HrDataAccess;
using Microsoft.EntityFrameworkCore;

const string ApiScopePolicy = "ApiScope";

var builder = WebApplication.CreateBuilder(args);

var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
var connectionString = builder.Configuration.GetConnectionString("postgres");

// Add services to the container.
builder.Services.AddDbContext<PostgresHrDbContext>(
    options => options.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly))
    );

var app = builder.Build();

app.Run();
