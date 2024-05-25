using HrServices.DTOs.Employees;
using HrServices.Entities;
using HrServices.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrServices.Abstractions
{
    interface IEmployeeService : ICrudService<CreateEmployee, UpdateEmployee, EmployeeDto, Employee>
    {
    }
}
