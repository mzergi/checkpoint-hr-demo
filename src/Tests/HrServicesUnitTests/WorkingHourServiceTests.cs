using HrServices.Abstractions.Repositories;
using HrServices.DTOs.WorkingHours;
using HrServices.Entities;
using HrServices.Services;
using Moq;
using Xunit;

namespace HrServicesUnitTests;

public class WorkingHourServiceTests
    : CrudServiceTestBase<WorkingHour, WorkingHourCreateDTO, WorkingHourUpdateDTO, IWorkingHoursRepository, WorkingHourService>
{
    public WorkingHourServiceTests()
        : base(
            addMaps: c => c.AddMaps(typeof(HrServices.AutoMapper.WorkingHourProfile).Assembly),
            serviceFactory: (repo, mapper) => new WorkingHourService(repo, mapper))
    { }

    protected override WorkingHourCreateDTO CreateDtoFactory() => new() { EmployeeId = Guid.NewGuid(), WorkingDay = DateTime.UtcNow.Date, From = DateTime.UtcNow, To = DateTime.UtcNow.AddHours(1) };
    protected override WorkingHourUpdateDTO UpdateDtoFactory() => new() { To = DateTime.UtcNow.AddHours(2) };
    protected override WorkingHour CreateEntityFactory() => new() { Id = Guid.NewGuid(), EmployeeId = Guid.NewGuid(), WorkingDay = DateTime.UtcNow.Date, From = DateTime.UtcNow, To = DateTime.UtcNow.AddHours(1) };

    protected override void AssertOnMappedCreate(WorkingHourCreateDTO dto, WorkingHour created)
    {
        Assert.Equal(dto.EmployeeId, created.EmployeeId);
        Assert.Equal(dto.WorkingDay, created.WorkingDay);
    }

    protected override void AssertOnUpdated(WorkingHour beforeUpdated, WorkingHour updated)
    {
        Assert.Equal(beforeUpdated.Id, updated.Id);
    }

    [Fact]
    public async Task GetByEmployee_DateRange_Works()
    {
        var empId = Guid.NewGuid();
        RepoMock.Setup(r => r.GetQueriedListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<WorkingHour, bool>>>(), null, null, false))
                .ReturnsAsync(new List<WorkingHour> { new() { EmployeeId = empId, WorkingDay = DateTime.UtcNow.Date } });

        var res = await Service.GetByEmployeeAsync(empId, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1));

        Assert.Single(res);
        Assert.Equal(empId, res.First().EmployeeId);
    }
}
