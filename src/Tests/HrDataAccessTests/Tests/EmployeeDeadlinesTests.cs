using HrDataAccess.Repositories;
using HrServices.Entities;

namespace HrDataAccessTests.Tests
{
    public class EmployeeDeadlinesTests : BaseRepositoryTest<BaseRepository<EmployeeDeadline>, EmployeeDeadline>
    {
        public override void CreateFake(out List<EmployeeDeadline> result, int number = 1)
        {
            bogusHelper.CreateFake(out List<EmployeeDeadline> employeeDeadlines, number);
            result = employeeDeadlines;
        }
    }
}
