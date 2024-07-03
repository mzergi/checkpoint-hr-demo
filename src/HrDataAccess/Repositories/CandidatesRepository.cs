using HrServices.Abstractions.Repositories;
using HrServices.Entities;

namespace HrDataAccess.Repositories
{
    public class CandidatesRepository : BaseRepository<Candidate>, ICandidatesRepository
    {
        public CandidatesRepository(PostgresHrDbContext context) : base(context) { }
    }
}
