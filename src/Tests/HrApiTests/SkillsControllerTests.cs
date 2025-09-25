using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using HrServices.DTOs;
using HrServices.DTOs.Skills;
using HrServices.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace HrApiTests;

public class SkillsControllerTests : ApiTestBase
{
    public SkillsControllerTests(WebApplicationFactory<Program> factory) : base(factory) { }

    private static SkillCreateDTO NewDto(string name = "C#") => new() { Name = name, Description = "Backend", Level = HrServices.Enums.SeniorityLevels.Medior };

    [Fact]
    public async Task Create_Works()
    {
        var resp = await Client.PostAsJsonAsync("/api/skills", NewDto());
        var entity = await ReadAsAsync<Skill>(resp);
        entity!.Name.Should().Be("C#");
    }

    [Fact]
    public async Task Get_By_Id_Works()
    {
        var created = await ReadAsAsync<Skill>(await Client.PostAsJsonAsync("/api/skills", NewDto()));
        var resp = await Client.GetAsync($"/api/skills/{created!.Id}");
        var fetched = await ReadAsAsync<Skill>(resp);
        fetched!.Id.Should().Be(created.Id);
    }

    [Fact]
    public async Task Get_Paged_Works()
    {
        await Client.PostAsJsonAsync("/api/skills", NewDto());
        var resp = await Client.GetAsync("/api/skills?CurrentPage=0&PageSize=10");
        var page = await ReadAsAsync<Page<Skill>>(resp);
        page!.Entries.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Update_Works()
    {
        var created = await ReadAsAsync<Skill>(await Client.PostAsJsonAsync("/api/skills", NewDto()));
        var resp = await Client.PutAsJsonAsync($"/api/skills/{created!.Id}", new SkillUpdateDTO { Name = "Updated", Description = "U", Level = HrServices.Enums.SeniorityLevels.Senior });
        var updated = await ReadAsAsync<Skill>(resp);
        updated!.Name.Should().Be("Updated");
    }

    [Fact]
    public async Task Delete_Works()
    {
        var created = await ReadAsAsync<Skill>(await Client.PostAsJsonAsync("/api/skills", NewDto()));
        var del = await Client.DeleteAsync($"/api/skills/{created!.Id}");
        del.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Create_Batch_Works()
    {
        var resp = await Client.PostAsJsonAsync("/api/skills/batch", new[] { NewDto("B") });
        var arr = await ReadAsAsync<Skill[]>(resp);
        arr!.Length.Should().Be(1);
    }

    [Fact]
    public async Task Get_Employees_Of_Skill_Works()
    {
        var skill = await ReadAsAsync<Skill>(await Client.PostAsJsonAsync("/api/skills", NewDto()));
        var emp = await ReadAsAsync<Employee>(await Client.PostAsJsonAsync("/api/employees",
            new HrServices.DTOs.Employees.EmployeeCreateDTO
            {
                FirstName = "E", LastName = "L", MothersName = "M", BirthDate = DateTime.UtcNow.AddYears(-20),
                Email = $"{Guid.NewGuid():N}@ex.com", PhoneNumber = "+1", Sex = "M", BirthPlace = "B", TaxNumber = "T",
                PersonalIdentifierCardNumber = "PIC", PassportNumber = "PP", SocialSecurityNumber = "SSN", Residence = "R", BankAccountNumber = "BA"
            }));
        await Client.PutAsJsonAsync($"/api/employees/{emp!.Id}/skills", new[] { skill!.Id });

        var resp = await Client.GetAsync($"/api/skills/{skill.Id}/employees");
        var emps = await ReadAsAsync<Employee[]>(resp);
        emps!.Length.Should().Be(1);
    }
}
