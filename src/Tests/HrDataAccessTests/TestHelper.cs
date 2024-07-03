using AutoBogus;
using Bogus;
using HrDataAccess;
using HrDataAccess.Repositories;
using HrServices.Abstractions.Repositories;
using HrServices.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrDataAccessTests
{
    public class TestHelper
    {
        // seeding data using Bogus https://www.nuget.org/packages/bogus#supportedframeworks-body-tab
        // https://stenbrinke.nl/blog/taking-ef-core-data-seeding-to-the-next-level-with-bogus/
        // entity to entity relationships end in an infinite loop when generating data -> generate them in your own methods
        private readonly PostgresHrDbContext postgresHrDbContext;
        public TestHelper()
        {
        }

        public ISkillsRepository GetSkillsRepository()
        {
            var builder = new DbContextOptionsBuilder<PostgresHrDbContext>();
            builder.UseInMemoryDatabase(databaseName: string.Format("CheckPointHr", DateTime.Now.Ticks));

            var dbContextOptions = builder.Options;
            var postgresHrDbContext = new PostgresHrDbContext(dbContextOptions);
            postgresHrDbContext.Database.EnsureDeleted();
            postgresHrDbContext.Database.EnsureCreated();
            return new SkillsRepository(postgresHrDbContext);
        }
    }
}
