using HrDataAccess.Repositories;
using HrServices.Entities;

namespace HrDataAccessTests.Tests
{
    public class CandidatesTests : BaseRepositoryTest<BaseRepository<Candidate>, Candidate>
    {
        public override void CreateFake(out List<Candidate> result, int number = 1)
        {
            bogusHelper.CreateFake(out List<Candidate> candidates, number);
            result = candidates;
        }
    }
}
