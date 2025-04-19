using System.Collections.ObjectModel;
using AutoMapper;
using HrApi.Abstraction;
using HrServices.Entities;
using HrServices.Abstractions.Services;
using HrServices.DTOs.Employees;
using HrServices.DTOs.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HrApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase, ICrudController<Employee, EmployeeCreateDTO, EmployeeUpdateDTO>
{
    private readonly IEmployeeService EmployeeService;
    private readonly ISkillService SkillService;
    private readonly IMapper Mapper;
    public EmployeesController(IEmployeeService employeeService, ISkillService skillService, IMapper mapper)
    {
        EmployeeService = employeeService;
        SkillService = skillService;
        Mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] PageFilters pageFilters)
    {
        var result = await EmployeeService.GetPagedAsync(pageFilters);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await EmployeeService.GetByIdAsync(id);

        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmployeeCreateDTO value)
    {
        var model = Mapper.Map<Employee>(value);
        var result = await EmployeeService.CreateAsync(model);

        return Ok(result);
    }
    
    [HttpPost("batch")]
    public async Task<IActionResult> Create([FromBody] ICollection<EmployeeCreateDTO> values)
    {
        List<Task<Employee>> tasks = new List<Task<Employee>>();
        values.ForEach(value =>
        {
            tasks.Add(EmployeeService.CreateAsync(value));
        });
        var results = await Task.WhenAll(tasks);
        return Ok(results);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeUpdateDTO value)
    {
        var result = await EmployeeService.UpdateAsync(id, value);

        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await EmployeeService.DeleteAsync(id);
    }

    [HttpPut("{id}/skills")]
    public async Task<IActionResult> UpdateSkillsForEmployee(Guid id, [FromBody] ICollection<Guid> skillIds)
    {
        var result = await EmployeeService.UpdateSkillsForEmployee(id, skillIds);
        return Ok(result);
        // todo: EmployeeSkills is not saved because we have to implement additional logic in the repo
    }
}