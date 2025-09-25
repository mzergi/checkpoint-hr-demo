using HrServices.Abstractions.Repositories;
using HrServices.DTOs.Skills;
using HrServices.Entities;
using HrServices.Services;
using Moq;
using Xunit;

namespace HrServicesUnitTests;

public class SkillServiceTests
    : CrudServiceTestBase<Skill, SkillCreateDTO, SkillUpdateDTO, ISkillsRepository, SkillService>
{
    public SkillServiceTests()
        : base(
            addMaps: c => c.AddMaps(typeof(HrServices.AutoMapper.SkillProfile).Assembly),
            serviceFactory: (repo, mapper) => new SkillService(repo, mapper))
    { }

    protected override SkillCreateDTO CreateDtoFactory() => new() { Name = "C#", Description = "Lang" };
    protected override SkillUpdateDTO UpdateDtoFactory() => new() { Description = "Updated" };
    protected override Skill CreateEntityFactory() => new() { Id = Guid.NewGuid(), Name = "X", Description = "Y", Employees = new List<Employee>() };

    protected override void AssertOnMappedCreate(SkillCreateDTO dto, Skill created)
    {
        Assert.Equal(dto.Name, created.Name);
        Assert.Equal(dto.Description, created.Description);
    }

    protected override void AssertOnUpdated(Skill beforeUpdated, Skill updated)
    {
        Assert.Equal(beforeUpdated.Id, updated.Id);
    }

    [Fact]
    public async Task GetEmployeesOfSkill_Returns()
    {
        var id = Guid.NewGuid();
        var skill = new Skill { Id = id, Employees = new List<Employee> { new() { Id = Guid.NewGuid() } } };
        RepoMock.Setup(r => r.GetByIdAsync(id, false)).ReturnsAsync(skill);

        var res = await Service.GetEmployeesOfSkill(id);

        Assert.Single(res);
    }

    [Fact]
    public async Task GetEmployeesOfSkill_Throws_WhenMissing()
    {
        RepoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>())).ReturnsAsync((Skill?)null);

        await Assert.ThrowsAsync<ArgumentException>(() => Service.GetEmployeesOfSkill(Guid.NewGuid()));
    }
}
