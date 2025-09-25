using HrServices.Abstractions.Repositories;
using HrServices.DTOs.EmployeeDocuments;
using HrServices.Entities;
using HrServices.Services;
using Moq;
using Xunit;

namespace HrServicesUnitTests;

public class EmployeeDocumentServiceTests
    : CrudServiceTestBase<EmployeeDocument, EmployeeDocumentCreateDTO, EmployeeDocumentUpdateDTO, IEmployeeDocumentsRepository, EmployeeDocumentService>
{
    public EmployeeDocumentServiceTests()
        : base(
            addMaps: c => c.AddMaps(typeof(HrServices.AutoMapper.EmployeeDocumentProfile).Assembly),
            serviceFactory: (repo, mapper) => new EmployeeDocumentService(repo, mapper))
    { }

    protected override EmployeeDocumentCreateDTO CreateDtoFactory() => new() { EmployeeId = Guid.NewGuid(), DocumentTypeId = Guid.NewGuid(), ValidFrom = DateTime.UtcNow };
    protected override EmployeeDocumentUpdateDTO UpdateDtoFactory() => new() { ValidTill = DateTime.UtcNow.Date.AddDays(10) };
    protected override EmployeeDocument CreateEntityFactory() => new() { Id = Guid.NewGuid(), EmployeeId = Guid.NewGuid(), DocumentTypeId = Guid.NewGuid(), ValidFrom = DateTime.UtcNow };

    protected override void AssertOnMappedCreate(EmployeeDocumentCreateDTO dto, EmployeeDocument created)
    {
        Assert.Equal(dto.EmployeeId, created.EmployeeId);
        Assert.Equal(dto.DocumentTypeId, created.DocumentTypeId);
    }

    protected override void AssertOnUpdated(EmployeeDocument beforeUpdated, EmployeeDocument updated)
    {
        Assert.Equal(beforeUpdated.Id, updated.Id);
    }

    [Fact]
    public async Task GetByEmployee_Works()
    {
        var empId = Guid.NewGuid();
        RepoMock.Setup(r => r.GetQueriedListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<EmployeeDocument, bool>>>(), null, null, false))
                .ReturnsAsync(new List<EmployeeDocument> { new() { EmployeeId = empId } });

        var res = await Service.GetByEmployeeAsync(empId);

        Assert.Single(res);
        Assert.Equal(empId, res.First().EmployeeId);
    }
}
