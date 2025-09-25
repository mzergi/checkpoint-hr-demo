using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using HrServices.DTOs;
using HrServices.DTOs.DocumentTypes;
using HrServices.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace HrApiTests;

public class DocumentTypesControllerTests : ApiTestBase
{
    public DocumentTypesControllerTests(WebApplicationFactory<Program> factory) : base(factory) { }

    private static DocumentTypeCreateDTO NewDto(string name = "Contract") => new() { Name = name };

    [Fact]
    public async Task Create_Works()
    {
        var resp = await Client.PostAsJsonAsync("/api/documenttypes", NewDto());
        var entity = await ReadAsAsync<DocumentType>(resp);
        entity!.Name.Should().Be("Contract");
    }

    [Fact]
    public async Task Get_By_Id_Works()
    {
        var created = await ReadAsAsync<DocumentType>(await Client.PostAsJsonAsync("/api/documenttypes", NewDto()));
        var resp = await Client.GetAsync($"/api/documenttypes/{created!.Id}");
        var fetched = await ReadAsAsync<DocumentType>(resp);
        fetched!.Id.Should().Be(created.Id);
    }

    [Fact]
    public async Task Get_Paged_Works()
    {
        await Client.PostAsJsonAsync("/api/documenttypes", NewDto());
        var resp = await Client.GetAsync("/api/documenttypes?CurrentPage=0&PageSize=10");
        var page = await ReadAsAsync<Page<DocumentType>>(resp);
        page!.Entries.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Update_Works()
    {
        var created = await ReadAsAsync<DocumentType>(await Client.PostAsJsonAsync("/api/documenttypes", NewDto()));
        var resp = await Client.PutAsJsonAsync($"/api/documenttypes/{created!.Id}", new DocumentTypeUpdateDTO { Name = "Agreement" });
        var updated = await ReadAsAsync<DocumentType>(resp);
        updated!.Name.Should().Be("Agreement");
    }

    [Fact]
    public async Task Delete_Works()
    {
        var created = await ReadAsAsync<DocumentType>(await Client.PostAsJsonAsync("/api/documenttypes", NewDto()));
        var del = await Client.DeleteAsync($"/api/documenttypes/{created!.Id}");
        del.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Create_Batch_Works()
    {
        var resp = await Client.PostAsJsonAsync("/api/documenttypes/batch", new[] { NewDto("A"), NewDto("B") });
        var arr = await ReadAsAsync<DocumentType[]>(resp);
        arr!.Length.Should().Be(2);
    }
}
