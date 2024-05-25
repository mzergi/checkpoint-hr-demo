using HrServices.Enums;

namespace HrServices.Entities
{
    public class Skill : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SeniorityLevels Level { get; set; }
        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
