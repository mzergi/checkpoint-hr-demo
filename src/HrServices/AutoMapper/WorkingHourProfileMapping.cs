using AutoMapper;
using HrServices.DTOs.WorkingHours;
using HrServices.Entities;

namespace HrServices.AutoMapper
{
    public class WorkingHourProfile : Profile
    {
        public WorkingHourProfile()
        {
            CreateMap<WorkingHourCreateDTO, WorkingHour>()
                .ForMember(d => d.BookedOnDay, o => o.MapFrom(s => s.BookedOnDay ?? DateTime.UtcNow));
            CreateMap<WorkingHour, WorkingHourUpdateDTO>().ReverseMap().ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
