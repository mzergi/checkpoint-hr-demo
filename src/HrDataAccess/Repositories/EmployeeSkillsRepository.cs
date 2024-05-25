using HrDataAccess.Abstraction;
using HrServices.Entities;

namespace HrDataAccess.Repositories
{
    public class EmployeeSkillsRepository : BaseRepository<EmployeeSkill>, IEmployeeSkillsRepository
    {
        public EmployeeSkillsRepository(PostgresHrDbContext context) : base(context) { }
    }
}
