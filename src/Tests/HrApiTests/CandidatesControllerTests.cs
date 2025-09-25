using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using HrServices.DTOs;
using HrServices.DTOs.Candidates;
using HrServices.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace HrApiTests;

public class CandidatesControllerTests : ApiTestBase
{
    public CandidatesControllerTests(WebApplicationFactory<Program> factory) : base(factory) { }

    private static CandidateCreateDTO NewCandidate() => new()
    {
        FirstName = "Alice",
        LastName = "Smith",
        MothersName = "Mary",
        BirthDate = DateTime.UtcNow.AddYears(-30),
        Email = $"{Guid.NewGuid():N}@example.com",
        PhoneNumber = "+100000000",
        Sex = "F"
    };

    [Fact]
    public async Task Create_Returns_Candidate()
    {
        var resp = await Client.PostAsJsonAsync("/api/candidates", NewCandidate());
        var entity = await ReadAsAsync<Candidate>(resp);
        entity!.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Get_By_Id_Returns_Entity()
    {
        var created = await ReadAsAsync<Candidate>(await Client.PostAsJsonAsync("/api/candidates", NewCandidate()));
        var resp = await Client.GetAsync($"/api/candidates/{created!.Id}");
        var fetched = await ReadAsAsync<Candidate>(resp);
        fetched!.Id.Should().Be(created.Id);
    }

    [Fact]
    public async Task Get_Paged_Returns_Page()
    {
        await Client.PostAsJsonAsync("/api/candidates", NewCandidate());
        var resp = await Client.GetAsync("/api/candidates?CurrentPage=0&PageSize=10");
        var page = await ReadAsAsync<Page<Candidate>>(resp);
        page!.Entries.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Update_Updates_Fields()
    {
        var created = await ReadAsAsync<Candidate>(await Client.PostAsJsonAsync("/api/candidates", NewCandidate()));
        var upd = new CandidateUpdateDTO { Comment = "Updated" };
        var resp = await Client.PutAsJsonAsync($"/api/candidates/{created!.Id}", upd);
        var updated = await ReadAsAsync<Candidate>(resp);
        updated!.Comment.Should().Be("Updated");
    }

    [Fact]
    public async Task Delete_Removes_Entity()
    {
        var created = await ReadAsAsync<Candidate>(await Client.PostAsJsonAsync("/api/candidates", NewCandidate()));
        var del = await Client.DeleteAsync($"/api/candidates/{created!.Id}");
        del.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Create_Batch_Returns_Array()
    {
        var resp = await Client.PostAsJsonAsync("/api/candidates/batch", new[]
        {
            NewCandidate(),
            NewCandidate()
        });
        var arr = await ReadAsAsync<Candidate[]>(resp);
        arr!.Length.Should().Be(2);
    }
}
