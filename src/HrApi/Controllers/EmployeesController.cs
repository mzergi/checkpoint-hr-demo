using AutoMapper;
using HrApi.Abstraction;
using HrServices.Entities;
using HrServices.Abstractions.Services;
using HrServices.DTOs.Employees;
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
    public async Task<IActionResult> GetPaged()
    {
        // todo: paging implementation
        var result = await Service.GetPagedAsync();

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
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EmployeeUpdateDTO value)
    {
        var entity = await Service.GetByIdAsync(id);
        var model = Mapper.Map(value, entity);
        var result = await Service.UpdateAsync(id, model);

        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await Service.DeleteAsync(id);
    }
}