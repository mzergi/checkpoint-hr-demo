using HrServices.DTOs.VacationTypes;
using HrServices.Entities;

namespace HrServices.Abstractions.Services
{
    public interface IVacationTypeService : ICrudService<VacationType, VacationTypeCreateDTO, VacationTypeUpdateDTO>
    {
    }
}
