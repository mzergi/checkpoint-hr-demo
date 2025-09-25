using HrServices.Abstractions.Repositories;
using HrServices.DTOs.Employments;
using HrServices.Entities;
using HrServices.Services;
using Moq;
using Xunit;

namespace HrServicesUnitTests;

public class EmploymentServiceTests
    : CrudServiceTestBase<Employment, EmploymentCreateDTO, EmploymentUpdateDTO, IEmploymentRepository, EmploymentService>
{
    public EmploymentServiceTests()
        : base(
            addMaps: c => c.AddMaps(typeof(HrServices.AutoMapper.EmploymentProfile).Assembly),
            serviceFactory: (repo, mapper) => new EmploymentService(repo, mapper))
    { }

    protected override EmploymentCreateDTO CreateDtoFactory() => new() { EmployeeId = Guid.NewGuid(), WeeklyWorkingHours = 40, Currency = "USD" };
    protected override EmploymentUpdateDTO UpdateDtoFactory() => new() { WeeklyWorkingHours = 20 };
    protected override Employment CreateEntityFactory() => new() { Id = Guid.NewGuid(), EmployeeId = Guid.NewGuid(), WeeklyWorkingHours = 40, Currency = "EUR" };

    protected override void AssertOnMappedCreate(EmploymentCreateDTO dto, Employment created)
    {
        Assert.Equal(dto.EmployeeId, created.EmployeeId);
        Assert.Equal(dto.WeeklyWorkingHours, created.WeeklyWorkingHours);
    }

    protected override void AssertOnUpdated(Employment beforeUpdated, Employment updated)
    {
        Assert.Equal(beforeUpdated.Id, updated.Id);
    }

    [Fact]
    public async Task GetByEmployee_Works()
    {
        var empId = Guid.NewGuid();
        RepoMock.Setup(r => r.GetQueriedListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Employment, bool>>>(), null, null, false))
                .ReturnsAsync(new List<Employment> { new() { EmployeeId = empId } });

        var res = await Service.GetByEmployeeAsync(empId);

        Assert.Single(res);
        Assert.Equal(empId, res.First().EmployeeId);
    }
}
