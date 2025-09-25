using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using HrServices.DTOs;
using HrServices.DTOs.VacationTypes;
using HrServices.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace HrApiTests;

public class VacationTypesControllerTests : ApiTestBase
{
    public VacationTypesControllerTests(WebApplicationFactory<Program> factory) : base(factory) { }

    private static VacationTypeCreateDTO NewDto(string name = "Paid") => new()
    {
        Name = name, ApplyForWholeDay = true, SalaryMultiplier = 1.0f, SubtractFromPaidVacation = true, SubtractFromWorkingHours = false
    };

    [Fact]
    public async Task Create_Works()
    {
        var resp = await Client.PostAsJsonAsync("/api/vacationtypes", NewDto());
        var entity = await ReadAsAsync<VacationType>(resp);
        entity!.Name.Should().Be("Paid");
    }

    [Fact]
    public async Task Get_By_Id_Works()
    {
        var created = await ReadAsAsync<VacationType>(await Client.PostAsJsonAsync("/api/vacationtypes", NewDto()));
        var resp = await Client.GetAsync($"/api/vacationtypes/{created!.Id}");
        var fetched = await ReadAsAsync<VacationType>(resp);
        fetched!.Id.Should().Be(created.Id);
    }

    [Fact]
    public async Task Get_Paged_Works()
    {
        await Client.PostAsJsonAsync("/api/vacationtypes", NewDto());
        var resp = await Client.GetAsync("/api/vacationtypes?CurrentPage=0&PageSize=10");
        var page = await ReadAsAsync<Page<VacationType>>(resp);
        page!.Entries.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Update_Works()
    {
        var created = await ReadAsAsync<VacationType>(await Client.PostAsJsonAsync("/api/vacationtypes", NewDto()));
        var resp = await Client.PutAsJsonAsync($"/api/vacationtypes/{created!.Id}", new VacationTypeUpdateDTO { Name = "Updated" });
        var updated = await ReadAsAsync<VacationType>(resp);
        updated!.Name.Should().Be("Updated");
    }

    [Fact]
    public async Task Delete_Works()
    {
        var created = await ReadAsAsync<VacationType>(await Client.PostAsJsonAsync("/api/vacationtypes", NewDto()));
        var del = await Client.DeleteAsync($"/api/vacationtypes/{created!.Id}");
        del.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Create_Batch_Works()
    {
        var resp = await Client.PostAsJsonAsync("/api/vacationtypes/batch", new[] { NewDto("Sick") });
        var arr = await ReadAsAsync<VacationType[]>(resp);
        arr!.Length.Should().Be(1);
    }
}
