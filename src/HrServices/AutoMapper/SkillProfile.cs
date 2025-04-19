using AutoMapper;
using HrServices.DTOs.Skills;
using HrServices.Entities;

namespace HrServices.AutoMapper;

public class SkillProfile : Profile
{
    public SkillProfile()
    {
        CreateMap<SkillCreateDTO, Skill>().ReverseMap();
        CreateMap<Skill, SkillUpdateDTO>().ReverseMap().ForAllMembers(opts =>
            opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}