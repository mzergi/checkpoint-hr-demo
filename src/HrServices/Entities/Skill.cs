using System.Text.Json.Serialization;
using HrServices.Enums;

namespace HrServices.Entities
{
    public class Skill : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SeniorityLevels Level { get; set; }
        // todo: provide these in separate endpoint
        [JsonIgnore]
        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }
        [JsonIgnore]
        public ICollection<Employee> Employees { get; set; }
    }
}
