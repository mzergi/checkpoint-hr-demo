using AutoMapper;
using HrServices.DTOs.Employments;
using HrServices.Entities;

namespace HrServices.AutoMapper
{
    public class EmploymentProfile : Profile
    {
        public EmploymentProfile()
        {
            CreateMap<EmploymentCreateDTO, Employment>().ReverseMap();
            CreateMap<Employment, EmploymentUpdateDTO>().ReverseMap().ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
