using HrServices.Abstractions.Repositories;
using HrServices.Entities;

namespace HrDataAccess.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(PostgresHrDbContext context) : base(context) { }
    }
}
