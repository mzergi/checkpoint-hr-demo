using HrServices.DTOs.ProjectHours;
using HrServices.Entities;

namespace HrServices.Abstractions.Services
{
    public interface IProjectHourService : ICrudService<ProjectHour, ProjectHourCreateDTO, ProjectHourUpdateDTO>
    {
        Task<ICollection<ProjectHour>> GetByProjectAsync(Guid projectId, DateTime? from = null, DateTime? to = null, CancellationToken ct = default);
        Task<ICollection<ProjectHour>> GetByEmployeeAsync(Guid employeeId, DateTime? from = null, DateTime? to = null, CancellationToken ct = default);
    }
}
