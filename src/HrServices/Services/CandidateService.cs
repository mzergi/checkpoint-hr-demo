using AutoMapper;
using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.DTOs.Candidates;
using HrServices.Entities;

namespace HrServices.Services
{
    public class CandidateService(ICandidatesRepository repository, IMapper mapper)
        : CrudService<Candidate, CandidateCreateDTO, CandidateUpdateDTO>(repository, mapper), ICandidateService
    {
    }
}
