using HrServices.Abstractions.Repositories;
using HrServices.DTOs.ProjectHours;
using HrServices.Entities;
using HrServices.Services;
using Moq;
using Xunit;

namespace HrServicesUnitTests;

public class ProjectHourServiceTests
    : CrudServiceTestBase<ProjectHour, ProjectHourCreateDTO, ProjectHourUpdateDTO, IProjectHoursRepository, ProjectHourService>
{
    public ProjectHourServiceTests()
        : base(
            addMaps: c => c.AddMaps(typeof(HrServices.AutoMapper.ProjectHourProfile).Assembly),
            serviceFactory: (repo, mapper) => new ProjectHourService(repo, mapper))
    { }

    protected override ProjectHourCreateDTO CreateDtoFactory() => new() { EmployeeId = Guid.NewGuid(), ProjectId = Guid.NewGuid(), WorkingDay = DateTime.UtcNow.Date };
    protected override ProjectHourUpdateDTO UpdateDtoFactory() => new() { WorkingDay = DateTime.UtcNow.Date.AddDays(1) };
    protected override ProjectHour CreateEntityFactory() => new() { Id = Guid.NewGuid(), EmployeeId = Guid.NewGuid(), ProjectId = Guid.NewGuid(), WorkingDay = DateTime.UtcNow.Date };

    protected override void AssertOnMappedCreate(ProjectHourCreateDTO dto, ProjectHour created)
    {
        Assert.Equal(dto.EmployeeId, created.EmployeeId);
        Assert.Equal(dto.ProjectId, created.ProjectId);
        Assert.Equal(dto.WorkingDay, created.WorkingDay);
    }

    protected override void AssertOnUpdated(ProjectHour beforeUpdated, ProjectHour updated)
    {
        Assert.Equal(beforeUpdated.Id, updated.Id);
    }

    [Fact]
    public async Task GetByProject_DateRange_Works()
    {
        var projectId = Guid.NewGuid();
        RepoMock.Setup(r => r.GetQueriedListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<ProjectHour, bool>>>(), null, null, false))
                .ReturnsAsync(new List<ProjectHour> { new() { ProjectId = projectId, WorkingDay = DateTime.UtcNow.Date } });

        var res = await Service.GetByProjectAsync(projectId, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1));

        Assert.Single(res);
        Assert.Equal(projectId, res.First().ProjectId);
    }

    [Fact]
    public async Task GetByEmployee_DateRange_Works()
    {
        var employeeId = Guid.NewGuid();
        RepoMock.Setup(r => r.GetQueriedListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<ProjectHour, bool>>>(), null, null, false))
                .ReturnsAsync(new List<ProjectHour> { new() { EmployeeId = employeeId, WorkingDay = DateTime.UtcNow.Date } });

        var res = await Service.GetByEmployeeAsync(employeeId, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1));

        Assert.Single(res);
        Assert.Equal(employeeId, res.First().EmployeeId);
    }
}
