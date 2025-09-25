using HrServices.DTOs.EmployeeDocuments;
using HrServices.Entities;

namespace HrServices.Abstractions.Services
{
    public interface IEmployeeDocumentService : ICrudService<EmployeeDocument, EmployeeDocumentCreateDTO, EmployeeDocumentUpdateDTO>
    {
        Task<ICollection<EmployeeDocument>> GetByEmployeeAsync(Guid employeeId, CancellationToken ct = default);
    }
}
