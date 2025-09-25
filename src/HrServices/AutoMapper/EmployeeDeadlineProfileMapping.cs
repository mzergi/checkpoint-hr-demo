using AutoMapper;
using HrServices.DTOs.EmployeeDeadlines;
using HrServices.Entities;

namespace HrServices.AutoMapper
{
    public class EmployeeDeadlineProfile : Profile
    {
        public EmployeeDeadlineProfile()
        {
            CreateMap<EmployeeDeadlineCreateDTO, EmployeeDeadline>().ReverseMap();
            CreateMap<EmployeeDeadline, EmployeeDeadlineUpdateDTO>().ReverseMap().ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
