using AutoMapper;
using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.DTOs.Employees;
using HrServices.Entities;

namespace HrServices.Services
{
    public class EmployeeService(IEmployeeRepository repository, IMapper mapper, ISkillService skillService)
        : CrudService<Employee, EmployeeCreateDTO, EmployeeUpdateDTO>(repository, mapper), IEmployeeService
    {
        public async Task<ICollection<Skill>> UpdateSkillsForEmployee(Guid id, ICollection<Guid> skillIds)
        {
            var employee = await GetByIdAsync(id);
            if (employee == null)
            {
                throw new ArgumentException("Employee not found.");
            }

            var skills = await skillService.GetByIdsAsync(skillIds);
            employee.Skills = skills;
            await repository.UpdateEntityAsync(employee);
            return employee.Skills;
        }
    }
}
