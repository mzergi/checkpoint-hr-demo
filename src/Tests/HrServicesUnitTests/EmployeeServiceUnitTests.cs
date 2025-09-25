using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.DTOs.Employees;
using HrServices.Entities;
using HrServices.Services;
using Moq;
using Xunit;

namespace HrServicesUnitTests;

public class EmployeeServiceTests
    : CrudServiceTestBase<Employee, EmployeeCreateDTO, EmployeeUpdateDTO, IEmployeeRepository, EmployeeService>
{
    private static readonly Mock<IEmployeeSkillsRepository> _employeeSkillsRepo = new();
    private static readonly Mock<ISkillService> _skillService = new();

    public EmployeeServiceTests()
        : base(
            addMaps: c => c.AddMaps(typeof(HrServices.AutoMapper.EmployeeProfile).Assembly),
            serviceFactory: (repo, mapper) => new EmployeeService(repo, _employeeSkillsRepo.Object, mapper, _skillService.Object))
    { }

    protected override EmployeeCreateDTO CreateDtoFactory() => new()
    {
        FirstName = "John",
        LastName = "Doe",
        MothersName = "Jane Doe",
        BirthDate = new DateTime(1990, 1, 2),
        Email = "john@doe.com",
        PhoneNumber = "+1-555-1234",
        Sex = "M",
        BirthPlace = "Budapest",
        TaxNumber = "TAX-123",
        PersonalIdentifierCardNumber = "ID123",
        PassportNumber = "P123456",
        SocialSecurityNumber = "SSN-999",
        Residence = "Main Street 1",
        BankAccountNumber = "HU12-3456-7890"
    };

    protected override EmployeeUpdateDTO UpdateDtoFactory() => new()
    {
        Email = "jane@doe.com",
        PhoneNumber = "+1-555-9999",
        Sex = "F",
        BirthPlace = "Vienna",
        TaxNumber = "TAX-456",
        PersonalIdentifierCardNumber = "ID999",
        PassportNumber = "P654321",
        SocialSecurityNumber = "SSN-111",
        Residence = "Second Street 2",
        BankAccountNumber = "HU98-7654-3210",
        PaidVacationsPerYear = 28
    };

    protected override Employee CreateEntityFactory() => new()
    {
        Id = Guid.NewGuid(),
        FirstName = "X",
        LastName = "Y",
        MothersName = "Z",
        BirthDate = new DateTime(1980, 5, 6),
        Email = "x@y.com",
        PhoneNumber = "+1-000-0000",
        Sex = "M",
        BirthPlace = "City",
        TaxNumber = "TAX-0",
        PersonalIdentifierCardNumber = "ID0",
        PassportNumber = "P0",
        SocialSecurityNumber = "SSN-0",
        Residence = "Somewhere",
        BankAccountNumber = "ACC-0",
        PaidVacationsPerYear = 20,
        Skills = new List<Skill>()
    };

    protected override void AssertOnMappedCreate(EmployeeCreateDTO dto, Employee created)
    {
        Assert.Equal(dto.FirstName, created.FirstName);
        Assert.Equal(dto.LastName, created.LastName);
        Assert.Equal(dto.MothersName, created.MothersName);
        Assert.Equal(dto.BirthDate, created.BirthDate);
        Assert.Equal(dto.Email, created.Email);
        Assert.Equal(dto.PhoneNumber, created.PhoneNumber);
        Assert.Equal(dto.Sex, created.Sex);
        Assert.Equal(dto.BirthPlace, created.BirthPlace);
        Assert.Equal(dto.TaxNumber, created.TaxNumber);
        Assert.Equal(dto.PersonalIdentifierCardNumber, created.PersonalIdentifierCardNumber);
        Assert.Equal(dto.PassportNumber, created.PassportNumber);
        Assert.Equal(dto.SocialSecurityNumber, created.SocialSecurityNumber);
        Assert.Equal(dto.Residence, created.Residence);
        Assert.Equal(dto.BankAccountNumber, created.BankAccountNumber);
    }

    protected override void AssertOnUpdated(Employee beforeUpdated, Employee updated)
    {
        Assert.Equal(beforeUpdated.Id, updated.Id);
        // Ensure optional fields can update when provided
        Assert.Equal("jane@doe.com", updated.Email);
        Assert.Equal("+1-555-9999", updated.PhoneNumber);
        Assert.Equal("F", updated.Sex);
        Assert.Equal("Vienna", updated.BirthPlace);
        Assert.Equal("TAX-456", updated.TaxNumber);
        Assert.Equal("ID999", updated.PersonalIdentifierCardNumber);
        Assert.Equal("P654321", updated.PassportNumber);
        Assert.Equal("SSN-111", updated.SocialSecurityNumber);
        Assert.Equal("Second Street 2", updated.Residence);
        Assert.Equal("HU98-7654-3210", updated.BankAccountNumber);
        Assert.Equal(28, updated.PaidVacationsPerYear);
    }

    [Fact]
    public async Task UpdateSkillsForEmployee_Updates()
    {
        var empId = Guid.NewGuid();
        var existing = new Employee { Id = empId, Skills = new List<Skill>() };
        RepoMock.Setup(r => r.GetByIdAsync(empId, false)).ReturnsAsync(existing);
        var skillIds = new[] { Guid.NewGuid(), Guid.NewGuid() };
        var skills = new List<Skill> { new() { Id = skillIds[0] }, new() { Id = skillIds[1] } };
        _skillService.Setup(s => s.GetByIdsAsync(It.Is<ICollection<Guid>>(c => c.SequenceEqual(skillIds)))).ReturnsAsync(skills);
        RepoMock.Setup(r => r.UpdateEntityAsync(existing)).ReturnsAsync(existing);

        var res = await Service.UpdateSkillsForEmployee(empId, skillIds);

        Assert.Equal(2, res.Count);
        RepoMock.Verify(r => r.UpdateEntityAsync(existing), Times.Once);
    }

    [Fact]
    public async Task UpdateSkillsForEmployee_Throws_WhenNotFound()
    {
        var empId = Guid.NewGuid();
        RepoMock.Setup(r => r.GetByIdAsync(empId, false)).ReturnsAsync((Employee?)null);

        await Assert.ThrowsAsync<InvalidOperationException>(() => Service.UpdateSkillsForEmployee(empId, Array.Empty<Guid>()));
    }
}
