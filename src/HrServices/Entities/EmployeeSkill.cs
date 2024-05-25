namespace HrServices.Entities
{
    public class EmployeeSkill : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public Guid SkillId { get; set; }
        public Employee Employee {  get; set; }
        public Skill Skill { get; set; }
        /// <summary>
        /// True if the person currently has this skill, false if not
        /// </summary>
        public bool IsActiveEntryForEmployee { get; set; }
        /// <summary>
        /// The date the person acquired this skill
        /// </summary>
        public DateTime ActiveEntryForEmployeeFrom { get; set; }
        /// <summary>
        /// The date the entry is no longer valid for the person
        /// </summary>
        public DateTime? ActiveEntryForEmployeeTill {  get; set; }
    }
}
