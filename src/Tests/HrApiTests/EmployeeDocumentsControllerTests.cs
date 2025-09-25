using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using HrServices.DTOs;
using HrServices.DTOs.DocumentTypes;
using HrServices.DTOs.EmployeeDocuments;
using HrServices.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace HrApiTests;

public class EmployeeDocumentsControllerTests : ApiTestBase
{
    public EmployeeDocumentsControllerTests(WebApplicationFactory<Program> factory) : base(factory) { }

    private async Task<(Employee Emp, DocumentType Dt)> SeedAsync()
    {
        var emp = (await ReadAsAsync<Employee>(await Client.PostAsJsonAsync("/api/employees",
            new HrServices.DTOs.Employees.EmployeeCreateDTO
            {
                FirstName = "E", LastName = "L", MothersName = "M", BirthDate = DateTime.UtcNow.AddYears(-20),
                Email = $"{Guid.NewGuid():N}@ex.com", PhoneNumber = "+1", Sex = "M", BirthPlace = "B", TaxNumber = "T",
                PersonalIdentifierCardNumber = "PIC", PassportNumber = "PP", SocialSecurityNumber = "SSN", Residence = "R", BankAccountNumber = "BA"
            })))!;
        var dt = (await ReadAsAsync<DocumentType>(await Client.PostAsJsonAsync("/api/documenttypes",
            new DocumentTypeCreateDTO { Name = "DT" })))!;
        return (emp, dt);
    }

    private static EmployeeDocumentCreateDTO NewDto(Guid empId, Guid dtId) => new()
    {
        EmployeeId = empId,
        DocumentTypeId = dtId,
        ValidFrom = DateTime.UtcNow.Date,
        ValidTill = null
    };

    [Fact]
    public async Task Create_Works()
    {
        var (emp, dt) = await SeedAsync();
        var resp = await Client.PostAsJsonAsync("/api/employeedocuments", NewDto(emp.Id, dt.Id));
        var entity = await ReadAsAsync<EmployeeDocument>(resp);
        entity!.EmployeeId.Should().Be(emp.Id);
    }

    [Fact]
    public async Task Get_By_Id_Works()
    {
        var (emp, dt) = await SeedAsync();
        var created = await ReadAsAsync<EmployeeDocument>(await Client.PostAsJsonAsync("/api/employeedocuments", NewDto(emp.Id, dt.Id)));
        var resp = await Client.GetAsync($"/api/employeedocuments/{created!.Id}");
        var fetched = await ReadAsAsync<EmployeeDocument>(resp);
        fetched!.Id.Should().Be(created.Id);
    }

    [Fact]
    public async Task Get_Paged_Works()
    {
        var (emp, dt) = await SeedAsync();
        await Client.PostAsJsonAsync("/api/employeedocuments", NewDto(emp.Id, dt.Id));
        var resp = await Client.GetAsync("/api/employeedocuments?CurrentPage=0&PageSize=10");
        var page = await ReadAsAsync<Page<EmployeeDocument>>(resp);
        page!.Entries.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Update_Works()
    {
        var (emp, dt) = await SeedAsync();
        var created = await ReadAsAsync<EmployeeDocument>(await Client.PostAsJsonAsync("/api/employeedocuments", NewDto(emp.Id, dt.Id)));
        var resp = await Client.PutAsJsonAsync($"/api/employeedocuments/{created!.Id}", new EmployeeDocumentUpdateDTO { ValidTill = DateTime.UtcNow.Date.AddDays(1) });
        var updated = await ReadAsAsync<EmployeeDocument>(resp);
        updated!.ValidTill.Should().NotBeNull();
    }

    [Fact]
    public async Task Delete_Works()
    {
        var (emp, dt) = await SeedAsync();
        var created = await ReadAsAsync<EmployeeDocument>(await Client.PostAsJsonAsync("/api/employeedocuments", NewDto(emp.Id, dt.Id)));
        var del = await Client.DeleteAsync($"/api/employeedocuments/{created!.Id}");
        del.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Create_Batch_Works()
    {
        var (emp, dt) = await SeedAsync();
        var resp = await Client.PostAsJsonAsync("/api/employeedocuments/batch", new[] { NewDto(emp.Id, dt.Id), NewDto(emp.Id, dt.Id) });
        var arr = await ReadAsAsync<EmployeeDocument[]>(resp);
        arr!.Length.Should().Be(2);
    }

    [Fact]
    public async Task Get_By_Employee_Works()
    {
        var (emp, dt) = await SeedAsync();
        await Client.PostAsJsonAsync("/api/employeedocuments", NewDto(emp.Id, dt.Id));
        var resp = await Client.GetAsync($"/api/employeedocuments/by-employee/{emp.Id}");
        var list = await ReadAsAsync<EmployeeDocument[]>(resp);
        list!.Should().NotBeEmpty();
    }
}
