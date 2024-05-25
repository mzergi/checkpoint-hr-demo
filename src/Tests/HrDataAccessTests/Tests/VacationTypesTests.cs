using HrDataAccess.Repositories;
using HrServices.Entities;

namespace HrDataAccessTests.Tests
{
    public class VacationTypesTests : BaseRepositoryTest<BaseRepository<VacationType>, VacationType>
    {
        public override void CreateFake(out List<VacationType> result, int number = 1)
        {
            bogusHelper.CreateFake(out List<VacationType> vacationTypes, number);
            result = vacationTypes;
        }
    }
}
