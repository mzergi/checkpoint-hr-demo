using HrServices.Abstractions.Repositories;
using HrServices.Entities;

namespace HrDataAccess.Repositories
{
    public class VacationTypesRepository : BaseRepository<VacationType>, IVacationTypesRepository
    {
        public VacationTypesRepository(PostgresHrDbContext context) : base(context) { }
    }
}
