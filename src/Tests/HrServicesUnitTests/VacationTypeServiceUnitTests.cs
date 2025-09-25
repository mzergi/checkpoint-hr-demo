using HrServices.Abstractions.Repositories;
using HrServices.DTOs.VacationTypes;
using HrServices.Entities;
using HrServices.Services;

namespace HrServicesUnitTests;

public class VacationTypeServiceTests
    : CrudServiceTestBase<VacationType, VacationTypeCreateDTO, VacationTypeUpdateDTO, IVacationTypesRepository, VacationTypeService>
{
    public VacationTypeServiceTests()
        : base(
            addMaps: c => c.AddMaps(typeof(HrServices.AutoMapper.VacationTypeProfile).Assembly),
            serviceFactory: (repo, mapper) => new VacationTypeService(repo, mapper))
    { }

    protected override VacationTypeCreateDTO CreateDtoFactory() => new() { Name = "Paid" };
    protected override VacationTypeUpdateDTO UpdateDtoFactory() => new() { Name = "Unpaid" };
    protected override VacationType CreateEntityFactory() => new() { Id = Guid.NewGuid(), Name = "X" };

    protected override void AssertOnMappedCreate(VacationTypeCreateDTO dto, VacationType created)
    {
        Assert.Equal(dto.Name, created.Name);
    }

    protected override void AssertOnUpdated(VacationType beforeUpdated, VacationType updated)
    {
        Assert.Equal(beforeUpdated.Id, updated.Id);
    }
}
