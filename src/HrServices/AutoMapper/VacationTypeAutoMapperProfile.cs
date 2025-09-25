using AutoMapper;
using HrServices.DTOs.VacationTypes;
using HrServices.Entities;

namespace HrServices.AutoMapper
{
    public class VacationTypeProfile : Profile
    {
        public VacationTypeProfile()
        {
            CreateMap<VacationTypeCreateDTO, VacationType>().ReverseMap();
            CreateMap<VacationType, VacationTypeUpdateDTO>().ReverseMap().ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
