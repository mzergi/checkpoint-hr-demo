using HrDataAccess.Abstraction;
using HrServices.Entities;

namespace HrDataAccess.Repositories
{
    public class SkillsRepository : BaseRepository<Skill>, ISkillsRepository
    {
        public SkillsRepository(PostgresHrDbContext context) : base(context) { }
    }
}
