using HrServices.Abstractions.Repositories;
using HrServices.Entities;

namespace HrDataAccess.Repositories
{
    public class VacationsRepository : BaseRepository<Vacation>, IVacationsRepository
    {
        public VacationsRepository(PostgresHrDbContext context) : base(context) { }
    }
}
