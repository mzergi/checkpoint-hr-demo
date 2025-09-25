using AutoMapper;
using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.DTOs.DocumentTypes;
using HrServices.Entities;

namespace HrServices.Services
{
    public class DocumentTypeService(IDocumentTypesRepository repository, IMapper mapper)
        : CrudService<DocumentType, DocumentTypeCreateDTO, DocumentTypeUpdateDTO>(repository, mapper), IDocumentTypeService
    {
    }
}
