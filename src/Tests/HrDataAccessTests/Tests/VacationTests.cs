using HrDataAccess.Repositories;
using HrServices.Entities;

namespace HrDataAccessTests.Tests
{
    public class VacationTests : BaseRepositoryTest<BaseRepository<Vacation>, Vacation>
    {
        public override void CreateFake(out List<Vacation> result, int number = 1)
        {
            bogusHelper.CreateFake(out List<Vacation> vacations, number);
            result = vacations;
        }
    }
}
