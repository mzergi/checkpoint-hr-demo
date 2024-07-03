using HrServices.Abstractions.Services;
using HrServices.DTOs.Employees;
using HrServices.Entities;
using HrServices.Models.Employees;

namespace HrServices.Services
{
    public class EmployeeService : CrudService<CreateEmployee, UpdateEmployee, EmployeeDto, Employee>, IEmployeeService
    {
    }
}
