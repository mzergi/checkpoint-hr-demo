using AutoMapper;
using HrServices.DTOs.Employees;
using HrServices.Entities;

namespace HrServices.AutoMapper;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<EmployeeCreateDTO, Employee>().ReverseMap();
        CreateMap<Employee, EmployeeUpdateDTO>().ReverseMap().ForAllMembers(opts => 
            opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}