using AutoMapper;
using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.DTOs.Employees;
using HrServices.Entities;

namespace HrServices.Services
{
    public class EmployeeService(IEmployeeRepository repository, IMapper mapper)
        : CrudService<Employee, EmployeeCreateDTO, EmployeeUpdateDTO>(repository, mapper), IEmployeeService;
}
