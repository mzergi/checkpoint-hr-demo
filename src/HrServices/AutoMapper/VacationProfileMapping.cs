using AutoMapper;
using HrServices.DTOs.Vacations;
using HrServices.Entities;
using HrServices.Enums;

namespace HrServices.AutoMapper
{
    public class VacationProfile : Profile
    {
        public VacationProfile()
        {
            CreateMap<VacationCreateDTO, Vacation>()
                .ForMember(d => d.VacationPlanningState, o => o.MapFrom(s => (VacationPlanningState)s.VacationPlanningState));
            CreateMap<Vacation, VacationUpdateDTO>().ReverseMap()
                .ForMember(d => d.VacationPlanningState, o => o.Condition((src, dest, srcMember) => srcMember != null))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
