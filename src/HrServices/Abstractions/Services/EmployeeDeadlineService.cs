using HrServices.DTOs.EmployeeDeadlines;
using HrServices.Entities;

namespace HrServices.Abstractions.Services
{
    public interface IEmployeeDeadlineService : ICrudService<EmployeeDeadline, EmployeeDeadlineCreateDTO, EmployeeDeadlineUpdateDTO>
    {
        Task<ICollection<EmployeeDeadline>> GetUpcomingAsync(DateTime until, Guid? employeeId = null, CancellationToken ct = default);
    }
}
