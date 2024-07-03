using HrServices.Abstractions.Repositories;
using HrServices.Entities;

namespace HrDataAccess.Repositories
{
    public class EmployeeDeadlinesRepository : BaseRepository<EmployeeDeadline>, IEmployeeDeadlinesRepository
    {
        public EmployeeDeadlinesRepository(PostgresHrDbContext context) : base(context) { }
    }
}
