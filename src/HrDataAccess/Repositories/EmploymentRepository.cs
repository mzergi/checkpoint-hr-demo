using HrDataAccess.Abstraction;
using HrServices.Entities;

namespace HrDataAccess.Repositories
{
    public class EmploymentRepository : BaseRepository<Employment>, IEmploymentRepository
    {
        public EmploymentRepository(PostgresHrDbContext context) : base(context) { }
    }
}
