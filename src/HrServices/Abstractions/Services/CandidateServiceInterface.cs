using HrServices.DTOs.Candidates;
using HrServices.Entities;

namespace HrServices.Abstractions.Services
{
    public interface ICandidateService : ICrudService<Candidate, CandidateCreateDTO, CandidateUpdateDTO>
    {
    }
}
