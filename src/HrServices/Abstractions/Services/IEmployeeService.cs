using HrServices.DTOs.Employees;
using HrServices.Entities;

namespace HrServices.Abstractions.Services
{
    public interface IEmployeeService : ICrudService<Employee, EmployeeCreateDTO, EmployeeUpdateDTO>
    {
        Task<ICollection<Skill>> UpdateSkillsForEmployee(Guid id, ICollection<Guid> skillIds);
    }
}
