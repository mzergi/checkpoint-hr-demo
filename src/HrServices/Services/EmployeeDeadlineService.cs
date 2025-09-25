using AutoMapper;
using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.DTOs.EmployeeDeadlines;
using HrServices.Entities;

namespace HrServices.Services
{
    public class EmployeeDeadlineService(IEmployeeDeadlinesRepository repository, IMapper mapper)
        : CrudService<EmployeeDeadline, EmployeeDeadlineCreateDTO, EmployeeDeadlineUpdateDTO>(repository, mapper), IEmployeeDeadlineService
    {
        public async Task<ICollection<EmployeeDeadline>> GetUpcomingAsync(DateTime until, Guid? employeeId = null, CancellationToken ct = default)
        {
            return await repository.GetQueriedListAsync(d =>
                d.DeadlineDate <= until && (!employeeId.HasValue || d.EmployeeId == employeeId.Value));
        }
    }
}
