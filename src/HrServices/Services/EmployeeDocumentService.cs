using AutoMapper;
using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.DTOs.EmployeeDocuments;
using HrServices.Entities;

namespace HrServices.Services
{
    public class EmployeeDocumentService(IEmployeeDocumentsRepository repository, IMapper mapper)
        : CrudService<EmployeeDocument, EmployeeDocumentCreateDTO, EmployeeDocumentUpdateDTO>(repository, mapper), IEmployeeDocumentService
    {
        public async Task<ICollection<EmployeeDocument>> GetByEmployeeAsync(Guid employeeId, CancellationToken ct = default)
        {
            return await repository.GetQueriedListAsync(d => d.EmployeeId == employeeId);
        }
    }
}
