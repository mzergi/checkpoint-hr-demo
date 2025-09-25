using HrServices.DTOs.Employments;
using HrServices.Entities;

namespace HrServices.Abstractions.Services
{
    public interface IEmploymentService : ICrudService<Employment, EmploymentCreateDTO, EmploymentUpdateDTO>
    {
        Task<ICollection<Employment>> GetByEmployeeAsync(Guid employeeId, CancellationToken ct = default);
    }
}
