using HrServices.Enums;

namespace HrServices.DTOs.Skills;

public class SkillUpdateDTO
{
    public Guid Id;
    public string Name { get; set; }
    public string Description { get; set; }
    public SeniorityLevels Level { get; set; }
}