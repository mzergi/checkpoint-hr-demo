using HrServices.Abstractions.Repositories;
using HrServices.DTOs.DocumentTypes;
using HrServices.Entities;
using HrServices.Services;

namespace HrServicesUnitTests;

public class DocumentTypeServiceTests
    : CrudServiceTestBase<DocumentType, DocumentTypeCreateDTO, DocumentTypeUpdateDTO, IDocumentTypesRepository, DocumentTypeService>
{
    public DocumentTypeServiceTests()
        : base(
            addMaps: c => c.AddMaps(typeof(HrServices.AutoMapper.DocumentTypeProfile).Assembly),
            serviceFactory: (repo, mapper) => new DocumentTypeService(repo, mapper))
    { }

    protected override DocumentTypeCreateDTO CreateDtoFactory() => new() { Name = "Passport" };
    protected override DocumentTypeUpdateDTO UpdateDtoFactory() => new() { Name = "ID" };
    protected override DocumentType CreateEntityFactory() => new() { Id = Guid.NewGuid(), Name = "X" };

    protected override void AssertOnMappedCreate(DocumentTypeCreateDTO dto, DocumentType created)
    {
        Assert.Equal(dto.Name, created.Name);
    }

    protected override void AssertOnUpdated(DocumentType beforeUpdated, DocumentType updated)
    {
        Assert.Equal(beforeUpdated.Id, updated.Id);
    }
}
