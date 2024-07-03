using HrServices.Abstractions.Repositories;
using HrServices.Entities;

namespace HrDataAccess.Repositories
{
    public class WorkingHoursRepository : BaseRepository<WorkingHour>, IWorkingHoursRepository
    {
        public WorkingHoursRepository(PostgresHrDbContext context) : base(context) { }
    }
}
