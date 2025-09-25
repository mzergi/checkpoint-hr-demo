using AutoMapper;
using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.DTOs.Vacations;
using HrServices.Entities;

namespace HrServices.Services
{
    public class VacationService(IVacationsRepository repository, IMapper mapper)
        : CrudService<Vacation, VacationCreateDTO, VacationUpdateDTO>(repository, mapper), IVacationService
    {
        public async Task<ICollection<Vacation>> GetByEmployeeAsync(Guid employeeId, DateTime? from = null, DateTime? to = null, CancellationToken ct = default)
        {
            return await repository.GetQueriedListAsync(v =>
                v.EmployeeId == employeeId &&
                (!from.HasValue || v.StartDate >= from.Value) &&
                (!to.HasValue || v.EndDate <= to.Value));
        }
    }
}
