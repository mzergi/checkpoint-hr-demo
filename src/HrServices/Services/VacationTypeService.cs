using AutoMapper;
using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.DTOs.VacationTypes;
using HrServices.Entities;

namespace HrServices.Services
{
    public class VacationTypeService(IVacationTypesRepository repository, IMapper mapper)
        : CrudService<VacationType, VacationTypeCreateDTO, VacationTypeUpdateDTO>(repository, mapper), IVacationTypeService
    {
    }
}
