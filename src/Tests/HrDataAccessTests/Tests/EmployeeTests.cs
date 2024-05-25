using HrDataAccess.Repositories;
using HrServices.Entities;

namespace HrDataAccessTests.Tests
{
    public class EmployeeTests : BaseRepositoryTest<BaseRepository<Employee>, Employee>
    {
        public override void CreateFake(out List<Employee> result, int number = 1)
        {
            bogusHelper.CreateFake(out List<Employee> employees, number);
            result = employees;
        }
    }
}
