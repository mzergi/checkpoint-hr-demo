using HrServices.Abstractions.Repositories;
using HrServices.DTOs.EmployeeDeadlines;
using HrServices.Entities;
using HrServices.Services;
using Moq;
using Xunit;

namespace HrServicesUnitTests;

public class EmployeeDeadlineServiceTests
    : CrudServiceTestBase<EmployeeDeadline, EmployeeDeadlineCreateDTO, EmployeeDeadlineUpdateDTO, IEmployeeDeadlinesRepository, EmployeeDeadlineService>
{
    public EmployeeDeadlineServiceTests()
        : base(
            addMaps: c => c.AddMaps(typeof(HrServices.AutoMapper.EmployeeDeadlineProfile).Assembly),
            serviceFactory: (repo, mapper) => new EmployeeDeadlineService(repo, mapper))
    { }

    protected override EmployeeDeadlineCreateDTO CreateDtoFactory() => new() { Topic = "Doc", DeadlineDate = DateTime.UtcNow };
    protected override EmployeeDeadlineUpdateDTO UpdateDtoFactory() => new() { Topic = "Changed" };
    protected override EmployeeDeadline CreateEntityFactory() => new() { Id = Guid.NewGuid(), Topic = "T", DeadlineDate = DateTime.UtcNow };

    protected override void AssertOnMappedCreate(EmployeeDeadlineCreateDTO dto, EmployeeDeadline created)
    {
        Assert.Equal(dto.Topic, created.Topic);
    }

    protected override void AssertOnUpdated(EmployeeDeadline beforeUpdated, EmployeeDeadline updated)
    {
        Assert.Equal(beforeUpdated.Id, updated.Id);
    }

    [Fact]
    public async Task GetUpcoming_Works()
    {
        var until = DateTime.UtcNow.AddDays(3);
        RepoMock.Setup(r => r.GetQueriedListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<EmployeeDeadline, bool>>>(), null, null, false))
                .ReturnsAsync(new List<EmployeeDeadline> { new() { DeadlineDate = until } });

        var res = await Service.GetUpcomingAsync(until);

        Assert.Single(res);
    }
}
