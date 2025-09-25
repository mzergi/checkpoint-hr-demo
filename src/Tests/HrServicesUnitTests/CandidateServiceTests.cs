using AutoMapper;
using HrServices.Abstractions.Repositories;
using HrServices.DTOs.Candidates;
using HrServices.Entities;
using HrServices.Services;
using Moq;
using Xunit;

namespace HrServicesUnitTests;

public class CandidateServiceTests
    : CrudServiceTestBase<Candidate, CandidateCreateDTO, CandidateUpdateDTO, ICandidatesRepository, CandidateService>
{
    public CandidateServiceTests()
        : base(
            addMaps: c => c.AddMaps(typeof(HrServices.AutoMapper.CandidateProfile).Assembly),
            serviceFactory: (repo, mapper) => new CandidateService(repo, mapper))
    { }

    protected override CandidateCreateDTO CreateDtoFactory() => new() { FirstName = "John", LastName = "Doe", Email = "john@doe.com" };
    protected override CandidateUpdateDTO UpdateDtoFactory() => new() { FirstName = "Jane" };
    protected override Candidate CreateEntityFactory() => new() { Id = Guid.NewGuid(), FirstName = "X", LastName = "Y", Email = "x@y.com" };

    protected override void AssertOnMappedCreate(CandidateCreateDTO dto, Candidate created)
    {
        Assert.Equal(dto.FirstName, created.FirstName);
        Assert.Equal(dto.LastName, created.LastName);
        Assert.Equal(dto.Email, created.Email);
    }

    protected override void AssertOnUpdated(Candidate beforeUpdated, Candidate updated)
    {
        Assert.Equal(beforeUpdated.Id, updated.Id);
    }
}
