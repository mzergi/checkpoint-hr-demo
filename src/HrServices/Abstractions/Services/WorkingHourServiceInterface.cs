using HrServices.DTOs.WorkingHours;
using HrServices.Entities;

namespace HrServices.Abstractions.Services
{
    public interface IWorkingHourService : ICrudService<WorkingHour, WorkingHourCreateDTO, WorkingHourUpdateDTO>
    {
        Task<ICollection<WorkingHour>> GetByEmployeeAsync(Guid employeeId, DateTime? from = null, DateTime? to = null, CancellationToken ct = default);
    }
}
