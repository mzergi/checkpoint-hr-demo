using HrDataAccess.Repositories;
using HrServices.Entities;

namespace HrDataAccessTests.Tests
{
    public class SkillsTests : BaseRepositoryTest<BaseRepository<Skill>, Skill>
    {
        private TestHelper helper;
        public SkillsTests()
        {
            helper = new TestHelper();
        }
        [Fact]
        public async Task SkillsRepository_CreateSkill_ShouldCreateSkill()
        {
            // Setup
            var skillsRepository = helper.GetSkillsRepository();
            bogusHelper.CreateFake(out List<Skill> fakes, 1);
            //var toAdd = fakes.First();
            var toAdd = BogusHelper.GetSkillFaker().Generate(1).First();
            // Act
            var created = await skillsRepository.AddAsync(toAdd);

            var result = await skillsRepository.GetByIdAsync(created.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(toAdd.Name, result.Name);
        }

        [Fact]
        public async Task SkillsRepository_CreateMany_ShouldCreateManySkills()
        {
            // Setup
            var skillsRepository = helper.GetSkillsRepository();
            var numberOfSkillsToCreate = 42;

            bogusHelper.CreateFake(out List<Skill> fakes, numberOfSkillsToCreate);
            var toAdd = fakes;

            // Act
            var created = await skillsRepository.AddRangeAsync(toAdd);
            var createdIds = created.Select(c => c.Id).ToList();

            var result = skillsRepository.GetQuery(s => createdIds.Contains(s.Id)).ToList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(toAdd.Count, result.Count);
            Assert.Contains(toAdd.First().Name, result.Select(x => x.Name));
            Assert.Contains(created.First().Name, result.Select(x => x.Name));
            Assert.Contains(toAdd.First().Name, created.Select(x => x.Name));
            Assert.Equal(toAdd.Select(x => x.Name).ToList(), result.Select(x => x.Name).ToList());
        }

        public override void CreateFake(out List<Skill> result, int number = 1)
        {
            bogusHelper.CreateFake(out List<Skill> skills, number);
            result = skills;
        }
    }
}