using HrServices.DTOs.Vacations;
using HrServices.Entities;

namespace HrServices.Abstractions.Services
{
    public interface IVacationService : ICrudService<Vacation, VacationCreateDTO, VacationUpdateDTO>
    {
        Task<ICollection<Vacation>> GetByEmployeeAsync(Guid employeeId, DateTime? from = null, DateTime? to = null, CancellationToken ct = default);
    }
}
