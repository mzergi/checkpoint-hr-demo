using System;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using HrServices.DTOs;
using HrServices.DTOs.ProjectHours;
using HrServices.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace HrApiTests;

public class ProjectHoursControllerTests : ApiTestBase
{
    public ProjectHoursControllerTests(WebApplicationFactory<Program> factory) : base(factory) { }

    private async Task<Employee> SeedEmployee() =>
        (await ReadAsAsync<Employee>(await Client.PostAsJsonAsync("/api/employees",
            new HrServices.DTOs.Employees.EmployeeCreateDTO
            {
                FirstName="E", LastName="L", MothersName="M", BirthDate=DateTime.UtcNow.AddYears(-20),
                Email=$"{Guid.NewGuid():N}@ex.com", PhoneNumber="+1", Sex="M", BirthPlace="B", TaxNumber="T",
                PersonalIdentifierCardNumber="PIC", PassportNumber="PP", SocialSecurityNumber="SSN", Residence="R", BankAccountNumber="BA"
            })))!;

    private static ProjectHourCreateDTO NewDto(Guid empId, DateTime day) => new()
    {
        EmployeeId = empId,
        Hours = 4,
        WorkingDay = day,
        BookedOnDay = day,
        ProjectId = Guid.NewGuid(),
        ProjectPositionId = Guid.NewGuid()
    };

    [Fact]
    public async Task Create_Works()
    {
        var emp = await SeedEmployee();
        var day = DateTime.UtcNow.Date;
        var resp = await Client.PostAsJsonAsync("/api/projecthours", NewDto(emp.Id, day));
        var entity = await ReadAsAsync<ProjectHour>(resp);
        entity!.EmployeeId.Should().Be(emp.Id);
    }

    [Fact]
    public async Task Get_By_Id_Works()
    {
        var emp = await SeedEmployee();
        var day = DateTime.UtcNow.Date;
        var created = await ReadAsAsync<ProjectHour>(await Client.PostAsJsonAsync("/api/projecthours", NewDto(emp.Id, day)));
        var resp = await Client.GetAsync($"/api/projecthours/{created!.Id}");
        var fetched = await ReadAsAsync<ProjectHour>(resp);
        fetched!.Id.Should().Be(created.Id);
    }

    [Fact]
    public async Task Get_Paged_Works()
    {
        var emp = await SeedEmployee();
        var day = DateTime.UtcNow.Date;
        await Client.PostAsJsonAsync("/api/projecthours", NewDto(emp.Id, day));
        var resp = await Client.GetAsync("/api/projecthours?CurrentPage=0&PageSize=10");
        var page = await ReadAsAsync<Page<ProjectHour>>(resp);
        page!.Entries.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Update_Works()
    {
        var emp = await SeedEmployee();
        var day = DateTime.UtcNow.Date;
        var created = await ReadAsAsync<ProjectHour>(await Client.PostAsJsonAsync("/api/projecthours", NewDto(emp.Id, day)));
        var resp = await Client.PutAsJsonAsync($"/api/projecthours/{created!.Id}", new ProjectHourUpdateDTO { Hours = 6 });
        var updated = await ReadAsAsync<ProjectHour>(resp);
        updated!.Hours.Should().Be(6);
    }

    [Fact]
    public async Task Delete_Works()
    {
        var emp = await SeedEmployee();
        var day = DateTime.UtcNow.Date;
        var created = await ReadAsAsync<ProjectHour>(await Client.PostAsJsonAsync("/api/projecthours", NewDto(emp.Id, day)));
        var del = await Client.DeleteAsync($"/api/projecthours/{created!.Id}");
        del.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Create_Batch_Works()
    {
        var emp = await SeedEmployee();
        var day = DateTime.UtcNow.Date;
        var resp = await Client.PostAsJsonAsync("/api/projecthours/batch", new[] { NewDto(emp.Id, day) });
        var arr = await ReadAsAsync<ProjectHour[]>(resp);
        arr!.Length.Should().Be(1);
    }

    [Fact]
    public async Task Get_By_Employee_Works()
    {
        var emp = await SeedEmployee();
        var day = DateTime.UtcNow.Date;
        var created = await ReadAsAsync<ProjectHour>(await Client.PostAsJsonAsync("/api/projecthours", NewDto(emp.Id, day)));
        var from = Uri.EscapeDataString(day.ToString("O"));
        var to = Uri.EscapeDataString(day.AddDays(1).ToString("O"));
        var resp = await Client.GetAsync($"/api/projecthours/by-employee/{emp.Id}?from={from}&to={to}");
        var list = await ReadAsAsync<ProjectHour[]>(resp);
        list!.Any(p => p.Id == created!.Id).Should().BeTrue();
    }

    [Fact]
    public async Task Get_By_Project_Works()
    {
        var emp = await SeedEmployee();
        var day = DateTime.UtcNow.Date;
        var created = await ReadAsAsync<ProjectHour>(await Client.PostAsJsonAsync("/api/projecthours", NewDto(emp.Id, day)));
        var from = Uri.EscapeDataString(day.ToString("O"));
        var to = Uri.EscapeDataString(day.AddDays(1).ToString("O"));
        var resp = await Client.GetAsync($"/api/projecthours/by-project/{created!.ProjectId}?from={from}&to={to}");
        var list = await ReadAsAsync<ProjectHour[]>(resp);
        list!.Any(p => p.Id == created!.Id).Should().BeTrue();
    }
}
