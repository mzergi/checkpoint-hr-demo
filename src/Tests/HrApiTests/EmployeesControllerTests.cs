using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using HrServices.DTOs;
using HrServices.DTOs.Employees;
using HrServices.DTOs.Skills;
using HrServices.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace HrApiTests;

public class EmployeesControllerTests : ApiTestBase
{
    public EmployeesControllerTests(WebApplicationFactory<Program> factory) : base(factory) { }

    private static EmployeeCreateDTO NewDto(string email) => new()
    {
        FirstName = "John",
        LastName = "Doe",
        MothersName = "Jane Doe",
        BirthDate = new DateTime(1990, 1, 1),
        Email = email,
        PhoneNumber = "+36301234567",
        Sex = "M",
        BirthPlace = "City",
        TaxNumber = "TAX123",
        PersonalIdentifierCardNumber = "ID123",
        PassportNumber = "P123",
        SocialSecurityNumber = "SSN",
        Residence = "Addr",
        BankAccountNumber = "ACC"
    };

    [Fact]
    public async Task Create_Works()
    {
        var resp = await Client.PostAsJsonAsync("/api/employees", NewDto($"{Guid.NewGuid():N}@test.local"));
        var entity = await ReadAsAsync<Employee>(resp);
        entity!.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Get_By_Id_Works()
    {
        var created = await ReadAsAsync<Employee>(await Client.PostAsJsonAsync("/api/employees", NewDto($"{Guid.NewGuid():N}@test.local")));
        var resp = await Client.GetAsync($"/api/employees/{created!.Id}");
        var fetched = await ReadAsAsync<Employee>(resp);
        fetched!.Id.Should().Be(created.Id);
    }

    [Fact]
    public async Task Get_Paged_Works()
    {
        await Client.PostAsJsonAsync("/api/employees", NewDto($"{Guid.NewGuid():N}@test.local"));
        var resp = await Client.GetAsync("/api/employees?CurrentPage=0&PageSize=10");
        var page = await ReadAsAsync<Page<Employee>>(resp);
        page!.Entries.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Update_Works()
    {
        var created = await ReadAsAsync<Employee>(await Client.PostAsJsonAsync("/api/employees", NewDto($"{Guid.NewGuid():N}@test.local")));
        var resp = await Client.PutAsJsonAsync($"/api/employees/{created!.Id}", new EmployeeUpdateDTO { Residence = "NewAddr", PaidVacationsPerYear = 25 });
        var updated = await ReadAsAsync<Employee>(resp);
        updated!.PaidVacationsPerYear.Should().Be(25);
    }

    [Fact]
    public async Task Delete_Works()
    {
        var created = await ReadAsAsync<Employee>(await Client.PostAsJsonAsync("/api/employees", NewDto($"{Guid.NewGuid():N}@test.local")));
        var del = await Client.DeleteAsync($"/api/employees/{created!.Id}");
        del.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Create_Batch_Works()
    {
        var resp = await Client.PostAsJsonAsync("/api/employees/batch", new[] { NewDto("b1@test.local") });
        var arr = await ReadAsAsync<Employee[]>(resp);
        arr!.Length.Should().Be(1);
    }

    [Fact]
    public async Task Update_Skills_For_Employee_Works()
    {
        var created = await ReadAsAsync<Employee>(await Client.PostAsJsonAsync("/api/employees", NewDto($"{Guid.NewGuid():N}@test.local")));
        var skill = await ReadAsAsync<Skill>(await Client.PostAsJsonAsync("/api/skills", new SkillCreateDTO { Name = "C#", Description = "Backend", Level = HrServices.Enums.SeniorityLevels.Junior }));
        var resp = await Client.PutAsJsonAsync($"/api/employees/{created!.Id}/skills", new[] { skill!.Id });
        var set = await ReadAsAsync<Skill[]>(resp);
        set!.Length.Should().Be(1);
    }
}
