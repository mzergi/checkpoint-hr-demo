using HrApi.Abstraction;
using HrServices.Entities;
using HrServices.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace HrApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController(IEmployeeService service) : ControllerBase, ICrudController<Employee>
{
    private IEmployeeService Service { get; set; } = service;
    
    [HttpGet]
    public async Task<IActionResult> GetPaged()
    {
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
    public async Task<IActionResult> Post([FromBody] Employee value)
    {
        // todo: create DTO
        var result = await Service.CreateAsync(value);

        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Employee value)
    {
        // todo: update DTO
        var result = await Service.UpdateAsync(value);

        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await Service.DeleteAsync(id);
    }
}