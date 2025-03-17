using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.Entities;

namespace HrServices.Services
{
    public class EmployeeService(IEmployeeRepository repository)
        : CrudService<Employee>(repository), IEmployeeService;
}
