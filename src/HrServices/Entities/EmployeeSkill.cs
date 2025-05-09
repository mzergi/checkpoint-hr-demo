using System.Text.Json.Serialization;

namespace HrServices.Entities
{
    public class EmployeeSkill : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        [JsonIgnore]
        public Employee Employee {  get; set; }
        public Guid SkillId { get; set; }
        [JsonIgnore]
        public Skill Skill { get; set; }
    }
}
