using AutoMapper;
using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.DTOs.ProjectHours;
using HrServices.Entities;

namespace HrServices.Services
{
    public class ProjectHourService(IProjectHoursRepository repository, IMapper mapper)
        : CrudService<ProjectHour, ProjectHourCreateDTO, ProjectHourUpdateDTO>(repository, mapper), IProjectHourService
    {
        public async Task<ICollection<ProjectHour>> GetByProjectAsync(Guid projectId, DateTime? from = null, DateTime? to = null, CancellationToken ct = default)
        {
            return await repository.GetQueriedListAsync(p =>
                p.ProjectId == projectId &&
                (!from.HasValue || p.WorkingDay >= from.Value) &&
                (!to.HasValue || p.WorkingDay <= to.Value));
        }

        public async Task<ICollection<ProjectHour>> GetByEmployeeAsync(Guid employeeId, DateTime? from = null, DateTime? to = null, CancellationToken ct = default)
        {
            return await repository.GetQueriedListAsync(p =>
                p.EmployeeId == employeeId &&
                (!from.HasValue || p.WorkingDay >= from.Value) &&
                (!to.HasValue || p.WorkingDay <= to.Value));
        }
    }
}
