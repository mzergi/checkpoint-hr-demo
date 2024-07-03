using HrServices.Abstractions.Repositories;
using HrServices.Entities;

namespace HrDataAccess.Repositories
{
    public class DocumentTypesRepository : BaseRepository<DocumentType>, IDocumentTypesRepository
    {
        public DocumentTypesRepository(PostgresHrDbContext context) : base(context) { }
    }
}
