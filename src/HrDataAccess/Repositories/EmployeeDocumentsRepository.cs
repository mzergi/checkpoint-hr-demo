using HrServices.Abstractions.Repositories;
using HrServices.Entities;

namespace HrDataAccess.Repositories
{
    public class EmployeeDocumentsRepository : BaseRepository<EmployeeDocument>, IEmployeeDocumentsRepository
    {
        public EmployeeDocumentsRepository(PostgresHrDbContext context) : base(context) { }
    }
}
