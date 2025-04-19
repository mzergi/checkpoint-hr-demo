using AutoMapper;
using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.DTOs.Skills;
using HrServices.Entities;

namespace HrServices.Services;

public class SkillService(ISkillsRepository repository, IMapper mapper)
    : CrudService<Skill, SkillCreateDTO, SkillUpdateDTO>(repository, mapper), ISkillService
{
    public async Task<ICollection<Employee>> GetEmployeesOfSkill(Guid id)
    {
        var skill = await repository.GetByIdAsync(id);
        if (skill == null)
        {
            throw new ArgumentException("Invalid ID when getting skill.");
        }

        return skill.Employees;
    }
}