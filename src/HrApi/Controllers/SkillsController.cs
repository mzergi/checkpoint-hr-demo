using AutoMapper;
using HrApi.Abstraction;
using HrServices.Abstractions.Services;
using HrServices.DTOs.Filters;
using HrServices.DTOs.Skills;
using HrServices.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HrApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SkillsController : ControllerBase, ICrudController<Skill, SkillCreateDTO, SkillUpdateDTO>
{
    private readonly ISkillService Service;
    private readonly IMapper Mapper;

    public SkillsController(IMapper mapper, ISkillService service)
    {
        Mapper = mapper;
        Service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] PageFilters pageFilters)
    {
        var result = await Service.GetPagedAsync(pageFilters);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await Service.GetByIdAsync(id);

        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SkillCreateDTO value)
    {
        var model = Mapper.Map<Skill>(value);
        var result = await Service.CreateAsync(model);

        return Ok(result);
    }
    
    [HttpPost("batch")]
    public async Task<IActionResult> Create([FromBody] ICollection<SkillCreateDTO> values)
    {
        List<Task<Skill>> tasks = new List<Task<Skill>>();
        values.ForEach(value =>
        {
            tasks.Add(Service.CreateAsync(value));
        });
        var results = await Task.WhenAll(tasks);
        return Ok(results);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] SkillUpdateDTO value)
    {
        var result = await Service.UpdateAsync(id, value);

        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await Service.DeleteAsync(id);
    }
}