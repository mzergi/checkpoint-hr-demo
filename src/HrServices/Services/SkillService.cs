using AutoMapper;
using HrServices.Abstractions.Repositories;
using HrServices.Abstractions.Services;
using HrServices.DTOs.Skills;
using HrServices.Entities;

namespace HrServices.Services;

public class SkillService(ISkillsRepository repository, IMapper mapper)
    : CrudService<Skill, SkillCreateDTO, SkillUpdateDTO>(repository, mapper), ISkillService;