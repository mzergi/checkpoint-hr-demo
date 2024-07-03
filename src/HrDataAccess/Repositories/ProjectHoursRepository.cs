using HrServices.Abstractions.Repositories;
using HrServices.Entities;

namespace HrDataAccess.Repositories
{
    public class ProjectHoursRepository : BaseRepository<ProjectHour>, IProjectHoursRepository
    {
        public ProjectHoursRepository(PostgresHrDbContext context) : base(context) { }
    }
}
