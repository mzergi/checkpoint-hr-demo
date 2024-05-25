using HrDataAccess.Repositories;
using HrServices.Entities;

namespace HrDataAccessTests.Tests
{
    public class EmployeeDocumentsTests : BaseRepositoryTest<BaseRepository<EmployeeDocument>, EmployeeDocument>
    {
        public override void CreateFake(out List<EmployeeDocument> result, int number = 1)
        {
            bogusHelper.CreateFake(out List<EmployeeDocument> employeeDocuments, number);
            result = employeeDocuments;
        }
    }
}
