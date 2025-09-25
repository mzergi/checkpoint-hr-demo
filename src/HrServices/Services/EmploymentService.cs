using AutoMapper;
using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.DTOs.Employments;
using HrServices.Entities;

namespace HrServices.Services
{
    public class EmploymentService(IEmploymentRepository repository, IMapper mapper)
        : CrudService<Employment, EmploymentCreateDTO, EmploymentUpdateDTO>(repository, mapper), IEmploymentService
    {
        public async Task<ICollection<Employment>> GetByEmployeeAsync(Guid employeeId, CancellationToken ct = default)
        {
            return await repository.GetQueriedListAsync(e => e.EmployeeId == employeeId);
        }
    }
}
