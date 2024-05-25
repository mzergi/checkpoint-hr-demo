using HrServices.Models.Employees;
using HrServices.Models.Persons;

namespace HrServices.DTOs.Employees
{
    public class EmployeeDto : EmployeeBase
    {
        public Guid Id { get; set; }
        public ICollection<Guid> Employments { get; set; }
        public ICollection<Guid> EmployeeSkills { get; set; }
    }
}
