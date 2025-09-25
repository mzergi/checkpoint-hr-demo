using AutoMapper;
using HrApi.Abstraction;
using HrServices.Abstractions.Services;
using HrServices.DTOs.Filters;
using HrServices.DTOs.Vacations;
using HrServices.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HrApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VacationsController : ControllerBase, ICrudController<Vacation, VacationCreateDTO, VacationUpdateDTO>
{
    private readonly IVacationService Service;
    private readonly IMapper Mapper;

    public VacationsController(IVacationService service, IMapper mapper)
    {
        Service = service;
        Mapper = mapper;
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

    [HttpGet("by-employee/{employeeId}")]
    public async Task<IActionResult> GetByEmployee(Guid employeeId, [FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null)
    {
        var result = await Service.GetByEmployeeAsync(employeeId, from, to);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VacationCreateDTO value)
    {
        var model = Mapper.Map<Vacation>(value);
        var result = await Service.CreateAsync(model);
        return Ok(result);
    }

    [HttpPost("batch")]
    public async Task<IActionResult> Create([FromBody] ICollection<VacationCreateDTO> values)
    {
        var tasks = values.Select(v => Service.CreateAsync(v)).ToArray();
        var results = await Task.WhenAll(tasks);
        return Ok(results);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] VacationUpdateDTO value)
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
