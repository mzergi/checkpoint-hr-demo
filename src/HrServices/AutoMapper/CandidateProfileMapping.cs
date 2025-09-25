using AutoMapper;
using HrServices.DTOs.Candidates;
using HrServices.Entities;

namespace HrServices.AutoMapper
{
    public class CandidateProfile : Profile
    {
        public CandidateProfile()
        {
            CreateMap<CandidateCreateDTO, Candidate>().ForMember(d => d.LastStateChange, o => o.Ignore());
            CreateMap<Candidate, CandidateUpdateDTO>().ReverseMap().ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
