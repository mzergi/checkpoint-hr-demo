using System;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using HrServices.DTOs;
using HrServices.DTOs.Vacations;
using HrServices.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace HrApiTests;

public class VacationsControllerTests : ApiTestBase
{
    public VacationsControllerTests(WebApplicationFactory<Program> factory) : base(factory) { }

    private async Task<(Employee Emp, VacationType Vt)> SeedAsync()
    {
        var emp = (await ReadAsAsync<Employee>(await Client.PostAsJsonAsync("/api/employees",
            new HrServices.DTOs.Employees.EmployeeCreateDTO
            {
                FirstName="E", LastName="L", MothersName="M", BirthDate=DateTime.UtcNow.AddYears(-20),
                Email=$"{Guid.NewGuid():N}@ex.com", PhoneNumber="+1", Sex="M", BirthPlace="B", TaxNumber="T",
                PersonalIdentifierCardNumber="PIC", PassportNumber="PP", SocialSecurityNumber="SSN", Residence="R", BankAccountNumber="BA"
            })))!;
        var vt = (await ReadAsAsync<VacationType>(await Client.PostAsJsonAsync("/api/vacationtypes",
            new HrServices.DTOs.VacationTypes.VacationTypeCreateDTO
            {
                Name="Paid", ApplyForWholeDay=true, SalaryMultiplier=1, SubtractFromPaidVacation=true, SubtractFromWorkingHours=false
            })))!;
        return (emp, vt);
    }

    private static VacationCreateDTO NewDto(Guid empId, Guid vtId, DateTime start, DateTime end) => new()
    {
        EmployeeId = empId, StartDate = start, EndDate = end, AbsenceHours = 16, VacationTypeId = vtId, VacationPlanningState = 0
    };

    [Fact]
    public async Task Create_Works()
    {
        var (emp, vt) = await SeedAsync();
        var start = DateTime.UtcNow.Date;
        var end = start.AddDays(2);
        var resp = await Client.PostAsJsonAsync("/api/vacations", NewDto(emp.Id, vt.Id, start, end));
        var entity = await ReadAsAsync<Vacation>(resp);
        entity!.EmployeeId.Should().Be(emp.Id);
    }

    [Fact]
    public async Task Get_By_Id_Works()
    {
        var (emp, vt) = await SeedAsync();
        var start = DateTime.UtcNow.Date;
        var end = start.AddDays(2);
        var created = await ReadAsAsync<Vacation>(await Client.PostAsJsonAsync("/api/vacations", NewDto(emp.Id, vt.Id, start, end)));
        var resp = await Client.GetAsync($"/api/vacations/{created!.Id}");
        var fetched = await ReadAsAsync<Vacation>(resp);
        fetched!.Id.Should().Be(created.Id);
    }

    [Fact]
    public async Task Get_Paged_Works()
    {
        var (emp, vt) = await SeedAsync();
        var start = DateTime.UtcNow.Date;
        var end = start.AddDays(2);
        await Client.PostAsJsonAsync("/api/vacations", NewDto(emp.Id, vt.Id, start, end));
        var resp = await Client.GetAsync("/api/vacations?CurrentPage=0&PageSize=10");
        var page = await ReadAsAsync<Page<Vacation>>(resp);
        page!.Entries.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Update_Works()
    {
        var (emp, vt) = await SeedAsync();
        var start = DateTime.UtcNow.Date;
        var end = start.AddDays(2);
        var created = await ReadAsAsync<Vacation>(await Client.PostAsJsonAsync("/api/vacations", NewDto(emp.Id, vt.Id, start, end)));
        var resp = await Client.PutAsJsonAsync($"/api/vacations/{created!.Id}", new VacationUpdateDTO { AbsenceHours = 20 });
        var updated = await ReadAsAsync<Vacation>(resp);
        updated!.AbsenceHours.Should().Be(20);
    }

    [Fact]
    public async Task Delete_Works()
    {
        var (emp, vt) = await SeedAsync();
        var start = DateTime.UtcNow.Date;
        var end = start.AddDays(2);
        var created = await ReadAsAsync<Vacation>(await Client.PostAsJsonAsync("/api/vacations", NewDto(emp.Id, vt.Id, start, end)));
        var del = await Client.DeleteAsync($"/api/vacations/{created!.Id}");
        del.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Create_Batch_Works()
    {
        var (emp, vt) = await SeedAsync();
        var start = DateTime.UtcNow.Date;
        var end = start.AddDays(2);
        var resp = await Client.PostAsJsonAsync("/api/vacations/batch", new[] { NewDto(emp.Id, vt.Id, start, end) });
        var arr = await ReadAsAsync<Vacation[]>(resp);
        arr!.Length.Should().Be(1);
    }

    [Fact]
    public async Task Get_By_Employee_Works()
    {
        var (emp, vt) = await SeedAsync();
        var start = DateTime.UtcNow.Date;
        var end = start.AddDays(2);
        var created = await ReadAsAsync<Vacation>(await Client.PostAsJsonAsync("/api/vacations", NewDto(emp.Id, vt.Id, start, end)));
        var from = Uri.EscapeDataString(start.ToString("O"));
        var to = Uri.EscapeDataString(end.ToString("O"));
        var resp = await Client.GetAsync($"/api/vacations/by-employee/{emp.Id}?from={from}&to={to}");
        var list = await ReadAsAsync<Vacation[]>(resp);
        list!.Any(v => v.Id == created!.Id).Should().BeTrue();
    }
}
