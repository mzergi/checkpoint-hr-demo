using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using HrServices.DTOs;
using HrServices.DTOs.EmployeeDeadlines;
using HrServices.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace HrApiTests;

public class EmployeeDeadlinesControllerTests : ApiTestBase
{
    public EmployeeDeadlinesControllerTests(WebApplicationFactory<Program> factory) : base(factory) { }

    private async Task<Employee> SeedEmployee()
    {
        var dto = new HrServices.DTOs.Employees.EmployeeCreateDTO
        {
            FirstName = "Emp",
            LastName = "One",
            MothersName = "Mom",
            BirthDate = DateTime.UtcNow.AddYears(-25),
            Email = $"{Guid.NewGuid():N}@ex.com",
            PhoneNumber = "+10",
            Sex = "M",
            BirthPlace = "City",
            TaxNumber = "TX",
            PersonalIdentifierCardNumber = "PIC",
            PassportNumber = "PP",
            SocialSecurityNumber = "SSN",
            Residence = "Addr",
            BankAccountNumber = "ACC"
        };
        var resp = await Client.PostAsJsonAsync("/api/employees", dto);
        return (await ReadAsAsync<Employee>(resp))!;
    }

    private static EmployeeDeadlineCreateDTO NewDto(Guid employeeId) => new()
    {
        Topic = "Review",
        Description = "Annual review",
        DeadlineDate = DateTime.UtcNow.AddDays(7),
        EmployeeId = employeeId
    };

    [Fact]
    public async Task Create_Works()
    {
        var emp = await SeedEmployee();
        var resp = await Client.PostAsJsonAsync("/api/employeedeadlines", NewDto(emp.Id));
        var entity = await ReadAsAsync<EmployeeDeadline>(resp);
        entity!.EmployeeId.Should().Be(emp.Id);
    }

    [Fact]
    public async Task Get_By_Id_Works()
    {
        var emp = await SeedEmployee();
        var created = await ReadAsAsync<EmployeeDeadline>(await Client.PostAsJsonAsync("/api/employeedeadlines", NewDto(emp.Id)));
        var resp = await Client.GetAsync($"/api/employeedeadlines/{created!.Id}");
        var fetched = await ReadAsAsync<EmployeeDeadline>(resp);
        fetched!.Id.Should().Be(created.Id);
    }

    [Fact]
    public async Task Get_Paged_Works()
    {
        var emp = await SeedEmployee();
        await Client.PostAsJsonAsync("/api/employeedeadlines", NewDto(emp.Id));
        var resp = await Client.GetAsync("/api/employeedeadlines?CurrentPage=0&PageSize=10");
        var page = await ReadAsAsync<Page<EmployeeDeadline>>(resp);
        page!.Entries.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Update_Works()
    {
        var emp = await SeedEmployee();
        var created = await ReadAsAsync<EmployeeDeadline>(await Client.PostAsJsonAsync("/api/employeedeadlines", NewDto(emp.Id)));
        var resp = await Client.PutAsJsonAsync($"/api/employeedeadlines/{created!.Id}", new EmployeeDeadlineUpdateDTO { Description = "Updated" });
        var updated = await ReadAsAsync<EmployeeDeadline>(resp);
        updated!.Description.Should().Be("Updated");
    }

    [Fact]
    public async Task Delete_Works()
    {
        var emp = await SeedEmployee();
        var created = await ReadAsAsync<EmployeeDeadline>(await Client.PostAsJsonAsync("/api/employeedeadlines", NewDto(emp.Id)));
        var del = await Client.DeleteAsync($"/api/employeedeadlines/{created!.Id}");
        del.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Create_Batch_Works()
    {
        var emp = await SeedEmployee();
        var resp = await Client.PostAsJsonAsync("/api/employeedeadlines/batch", new[]
        {
            NewDto(emp.Id),
            NewDto(emp.Id)
        });
        var arr = await ReadAsAsync<EmployeeDeadline[]>(resp);
        arr!.Length.Should().Be(2);
    }

    [Fact]
    public async Task Get_Upcoming_Works()
    {
        var emp = await SeedEmployee();
        await Client.PostAsJsonAsync("/api/employeedeadlines", NewDto(emp.Id));
        var until = Uri.EscapeDataString(DateTime.UtcNow.AddDays(10).ToString("O"));
        var resp = await Client.GetAsync($"/api/employeedeadlines/upcoming?until={until}&employeeId={emp.Id}");
        var arr = await ReadAsAsync<EmployeeDeadline[]>(resp);
        arr!.Should().NotBeEmpty();
    }
}
