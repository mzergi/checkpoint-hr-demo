using HrServices.Abstractions.Repositories;
using HrServices.DTOs.Vacations;
using HrServices.Entities;
using HrServices.Services;
using Moq;
using Xunit;

namespace HrServicesUnitTests;

public class VacationServiceTests
    : CrudServiceTestBase<Vacation, VacationCreateDTO, VacationUpdateDTO, IVacationsRepository, VacationService>
{
    public VacationServiceTests()
        : base(
            addMaps: c => c.AddMaps(typeof(HrServices.AutoMapper.VacationProfile).Assembly),
            serviceFactory: (repo, mapper) => new VacationService(repo, mapper))
    { }

    protected override VacationCreateDTO CreateDtoFactory() => new() { EmployeeId = Guid.NewGuid(), StartDate = DateTime.UtcNow.Date, EndDate = DateTime.UtcNow.Date.AddDays(1) };
    protected override VacationUpdateDTO UpdateDtoFactory() => new() { EndDate = DateTime.UtcNow.Date.AddDays(2) };
    protected override Vacation CreateEntityFactory() => new() { Id = Guid.NewGuid(), EmployeeId = Guid.NewGuid(), StartDate = DateTime.UtcNow.Date, EndDate = DateTime.UtcNow.Date.AddDays(1) };

    protected override void AssertOnMappedCreate(VacationCreateDTO dto, Vacation created)
    {
        Assert.Equal(dto.EmployeeId, created.EmployeeId);
        Assert.Equal(dto.StartDate, created.StartDate);
        Assert.Equal(dto.EndDate, created.EndDate);
    }

    protected override void AssertOnUpdated(Vacation beforeUpdated, Vacation updated)
    {
        Assert.Equal(beforeUpdated.Id, updated.Id);
    }

    [Fact]
    public async Task GetByEmployee_DateRange_Works()
    {
        var empId = Guid.NewGuid();
        RepoMock.Setup(r => r.GetQueriedListAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Vacation, bool>>>(), null, null, false))
                .ReturnsAsync(new List<Vacation> { new() { EmployeeId = empId, StartDate = DateTime.UtcNow.Date, EndDate = DateTime.UtcNow.Date.AddDays(1) } });

        var res = await Service.GetByEmployeeAsync(empId, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1));

        Assert.Single(res);
        Assert.Equal(empId, res.First().EmployeeId);
    }
}
