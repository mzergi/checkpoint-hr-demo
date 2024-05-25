using HrServices.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrDataAccess
{
    public class PostgresHrDbContext : DbContext
    {
        public DbSet<Employee> Employees {  get; set; }
        public DbSet<Employment> Employments {  get; set; }
        public DbSet<Skill> Skills {  get; set; }
        public DbSet<EmployeeSkill> EmployeeSkills {  get; set; }
        public DbSet<Vacation> Vacations {  get; set; }
        public DbSet<VacationType> VacationTypes {  get; set; }
        public DbSet<WorkingHour> WorkingHours {  get; set; }
        public DbSet<ProjectHour> ProjectHours {  get; set; }
        public DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<EmployeeDeadline> EmployeeDeadlines { get; set; }
        public DbSet<Candidate> Candidates { get; set; }

        public PostgresHrDbContext(DbContextOptions<PostgresHrDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employment>()
                .HasOne(e => e.Employee)
                .WithMany(employee => employee.Employments);
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Skills)
                .WithMany(s => s.Employees)
                .UsingEntity("EmployeeSkill");
        }
    }
}
