using AutoMapper;
using HrServices.DTOs.ProjectHours;
using HrServices.Entities;

namespace HrServices.AutoMapper
{
    public class ProjectHourProfile : Profile
    {
        public ProjectHourProfile()
        {
            CreateMap<ProjectHourCreateDTO, ProjectHour>().ReverseMap();
            CreateMap<ProjectHour, ProjectHourUpdateDTO>().ReverseMap().ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
