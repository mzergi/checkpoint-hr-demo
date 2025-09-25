using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HrDataAccess;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]

namespace HrApiTests;

// Base for API tests. Ensures clean DB before each test and provides HttpClient + scoped services.
public abstract class ApiTestBase : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    protected readonly WebApplicationFactory<Program> Factory;
    protected HttpClient Client { get; private set; } = default!;

    protected ApiTestBase(WebApplicationFactory<Program> factory)
    {
        Factory = factory.WithWebHostBuilder(_ => { /* override here if needed */ });
    }

    public virtual async Task InitializeAsync()
    {
        Client = Factory.CreateClient();

        // Clean database before each test
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<PostgresHrDbContext>();

        // Ensure database exists and apply migrations if needed
        await db.Database.MigrateAsync();

        // Truncate data (soft-delete model, but tests need clean slate)
        // Order matters due to FKs
        db.EmployeeDocuments.RemoveRange(db.EmployeeDocuments);
        db.EmployeeDeadlines.RemoveRange(db.EmployeeDeadlines);
        db.ProjectHours.RemoveRange(db.ProjectHours);
        db.WorkingHours.RemoveRange(db.WorkingHours);
        db.Employments.RemoveRange(db.Employments);
        db.Vacations.RemoveRange(db.Vacations);
        db.Skills.RemoveRange(db.Skills);
        db.DocumentTypes.RemoveRange(db.DocumentTypes);
        db.VacationTypes.RemoveRange(db.VacationTypes);
        db.Employees.RemoveRange(db.Employees);
        db.Candidates.RemoveRange(db.Candidates);
        await db.SaveChangesAsync();
    }

    public virtual Task DisposeAsync()
    {
        Client.Dispose();
        return Task.CompletedTask;
    }

    protected async Task<T?> ReadAsAsync<T>(HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }
}
