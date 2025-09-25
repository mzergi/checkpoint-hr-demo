using AutoMapper;
using HrApi.Abstraction;
using HrServices.Abstractions.Services;
using HrServices.DTOs.EmployeeDeadlines;
using HrServices.DTOs.Filters;
using HrServices.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HrApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeDeadlinesController : ControllerBase, ICrudController<EmployeeDeadline, EmployeeDeadlineCreateDTO, EmployeeDeadlineUpdateDTO>
{
    private readonly IEmployeeDeadlineService Service;
    private readonly IMapper Mapper;

    public EmployeeDeadlinesController(IEmployeeDeadlineService service, IMapper mapper)
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmployeeDeadlineCreateDTO value)
    {
        var model = Mapper.Map<EmployeeDeadline>(value);
        var result = await Service.CreateAsync(model);
        return Ok(result);
    }

    [HttpPost("batch")]
    public async Task<IActionResult> Create([FromBody] ICollection<EmployeeDeadlineCreateDTO> values)
    {
        var tasks = values.Select(v => Service.CreateAsync(v)).ToArray();
        var results = await Task.WhenAll(tasks);
        return Ok(results);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeDeadlineUpdateDTO value)
    {
        var result = await Service.UpdateAsync(id, value);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await Service.DeleteAsync(id);
    }

    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcoming([FromQuery] DateTime until, [FromQuery] Guid? employeeId = null)
    {
        var result = await Service.GetUpcomingAsync(until, employeeId);
        return Ok(result);
    }
}
