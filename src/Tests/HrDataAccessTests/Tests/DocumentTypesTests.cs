using HrDataAccess.Repositories;
using HrServices.Entities;

namespace HrDataAccessTests.Tests
{
    public class DocumentTypesTests : BaseRepositoryTest<BaseRepository<DocumentType>, DocumentType>
    {
        public override void CreateFake(out List<DocumentType> result, int number = 1)
        {
            bogusHelper.CreateFake(out List<DocumentType> documentTypes, number);
            result = documentTypes;
        }
    }
}
