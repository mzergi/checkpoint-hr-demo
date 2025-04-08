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
    private IEmployeeService Service;
    private IMapper Mapper;

    public EmployeesController(IEmployeeService service, IMapper mapper)
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
    public async Task<IActionResult> Create([FromBody] EmployeeCreateDTO value)
    {
        var model = Mapper.Map<Employee>(value);
        var result = await Service.CreateAsync(model);

        return Ok(result);
    }
    
    [HttpPost("batch")]
    public async Task<IActionResult> Create([FromBody] ICollection<EmployeeCreateDTO> values)
    {
        List<Task<Employee>> tasks = new List<Task<Employee>>();
        values.ForEach(value =>
        {
            tasks.Add(Service.CreateAsync(value));
        });
        var results = await Task.WhenAll(tasks);
        return Ok(results);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeUpdateDTO value)
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