using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using HrServices.DTOs;
using HrServices.DTOs.Employments;
using HrServices.Entities;
using HrServices.Enums;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace HrApiTests;

public class EmploymentsControllerTests : ApiTestBase
{
    public EmploymentsControllerTests(WebApplicationFactory<Program> factory) : base(factory) { }

    private async Task<Employee> SeedEmployee() =>
        (await ReadAsAsync<Employee>(await Client.PostAsJsonAsync("/api/employees",
            new HrServices.DTOs.Employees.EmployeeCreateDTO
            {
                FirstName = "E", LastName = "L", MothersName = "M", BirthDate = DateTime.UtcNow.AddYears(-22),
                Email = $"{Guid.NewGuid():N}@ex.com", PhoneNumber = "+1", Sex = "M", BirthPlace = "B", TaxNumber = "T",
                PersonalIdentifierCardNumber = "PIC", PassportNumber = "PP", SocialSecurityNumber = "SSN", Residence = "R", BankAccountNumber = "BA"
            })))!;

    private static EmploymentCreateDTO NewDto(Guid empId) => new()
    {
        EmployeeId = empId,
        WeeklyWorkingHours = 40,
        EmploymentType = EmploymentTypes.FullTime,
        ContractStart = DateTime.UtcNow.Date,
        ContractEnd = null,
        Currency = "USD",
        Salary = 1000,
        HourlyRate = 0,
        CostCenter = "CC",
        PaidVacationsPerYear = 25,
        HomeDepartmentId = Guid.NewGuid()
    };

    [Fact]
    public async Task Create_Works()
    {
        var emp = await SeedEmployee();
        var resp = await Client.PostAsJsonAsync("/api/employments", NewDto(emp.Id));
        var entity = await ReadAsAsync<Employment>(resp);
        entity!.EmployeeId.Should().Be(emp.Id);
    }

    [Fact]
    public async Task Get_By_Id_Works()
    {
        var emp = await SeedEmployee();
        var created = await ReadAsAsync<Employment>(await Client.PostAsJsonAsync("/api/employments", NewDto(emp.Id)));
        var resp = await Client.GetAsync($"/api/employments/{created!.Id}");
        var fetched = await ReadAsAsync<Employment>(resp);
        fetched!.Id.Should().Be(created.Id);
    }

    [Fact]
    public async Task Get_Paged_Works()
    {
        var emp = await SeedEmployee();
        await Client.PostAsJsonAsync("/api/employments", NewDto(emp.Id));
        var resp = await Client.GetAsync("/api/employments?CurrentPage=0&PageSize=10");
        var page = await ReadAsAsync<Page<Employment>>(resp);
        page!.Entries.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Update_Works()
    {
        var emp = await SeedEmployee();
        var created = await ReadAsAsync<Employment>(await Client.PostAsJsonAsync("/api/employments", NewDto(emp.Id)));
        var resp = await Client.PutAsJsonAsync($"/api/employments/{created!.Id}", new EmploymentUpdateDTO { WeeklyWorkingHours = 20 });
        var updated = await ReadAsAsync<Employment>(resp);
        updated!.WeeklyWorkingHours.Should().Be(20);
    }

    [Fact]
    public async Task Delete_Works()
    {
        var emp = await SeedEmployee();
        var created = await ReadAsAsync<Employment>(await Client.PostAsJsonAsync("/api/employments", NewDto(emp.Id)));
        var del = await Client.DeleteAsync($"/api/employments/{created!.Id}");
        del.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Create_Batch_Works()
    {
        var emp = await SeedEmployee();
        var resp = await Client.PostAsJsonAsync("/api/employments/batch", new[] { NewDto(emp.Id) });
        var arr = await ReadAsAsync<Employment[]>(resp);
        arr!.Length.Should().Be(1);
    }

    [Fact]
    public async Task Get_By_Employee_Works()
    {
        var emp = await SeedEmployee();
        await Client.PostAsJsonAsync("/api/employments", NewDto(emp.Id));
        var resp = await Client.GetAsync($"/api/employments/by-employee/{emp.Id}");
        var list = await ReadAsAsync<Employment[]>(resp);
        list!.Length.Should().Be(1);
    }
}
