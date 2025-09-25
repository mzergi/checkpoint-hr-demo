using AutoMapper;
using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.DTOs.WorkingHours;
using HrServices.Entities;

namespace HrServices.Services
{
    public class WorkingHourService(IWorkingHoursRepository repository, IMapper mapper)
        : CrudService<WorkingHour, WorkingHourCreateDTO, WorkingHourUpdateDTO>(repository, mapper), IWorkingHourService
    {
        public async Task<ICollection<WorkingHour>> GetByEmployeeAsync(Guid employeeId, DateTime? from = null, DateTime? to = null, CancellationToken ct = default)
        {
            return await repository.GetQueriedListAsync(w =>
                w.EmployeeId == employeeId &&
                (!from.HasValue || w.WorkingDay >= from.Value) &&
                (!to.HasValue || w.WorkingDay <= to.Value));
        }
    }
}
