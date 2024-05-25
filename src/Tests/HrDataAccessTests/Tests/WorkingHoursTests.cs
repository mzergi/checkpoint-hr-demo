using HrDataAccess.Repositories;
using HrServices.Entities;

namespace HrDataAccessTests.Tests
{
    public class WorkingHoursTests : BaseRepositoryTest<BaseRepository<WorkingHour>, WorkingHour>
    {
        public override void CreateFake(out List<WorkingHour> result, int number = 1)
        {
            bogusHelper.CreateFake(out List<WorkingHour> workingHours, number);
            result = workingHours;
        }
    }
}
