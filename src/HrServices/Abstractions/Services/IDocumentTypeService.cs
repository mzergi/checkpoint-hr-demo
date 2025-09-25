using HrServices.DTOs.DocumentTypes;
using HrServices.Entities;

namespace HrServices.Abstractions.Services
{
    public interface IDocumentTypeService : ICrudService<DocumentType, DocumentTypeCreateDTO, DocumentTypeUpdateDTO>
    {
    }
}
