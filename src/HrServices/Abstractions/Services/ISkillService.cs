using HrServices.DTOs.Skills;
using HrServices.Entities;

namespace HrServices.Abstractions.Services
{
    public interface ISkillService : ICrudService<Skill, SkillCreateDTO, SkillUpdateDTO>
    {
        Task<ICollection<Employee>> GetEmployeesOfSkill(Guid id);

    }
}