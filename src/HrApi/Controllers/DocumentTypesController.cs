using AutoMapper;
using HrApi.Abstraction;
using HrServices.Abstractions.Services;
using HrServices.DTOs.DocumentTypes;
using HrServices.DTOs.Filters;
using HrServices.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HrApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentTypesController : ControllerBase, ICrudController<DocumentType, DocumentTypeCreateDTO, DocumentTypeUpdateDTO>
{
    private readonly IDocumentTypeService Service;
    private readonly IMapper Mapper;

    public DocumentTypesController(IDocumentTypeService service, IMapper mapper)
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
    public async Task<IActionResult> Create([FromBody] DocumentTypeCreateDTO value)
    {
        var model = Mapper.Map<DocumentType>(value);
        var result = await Service.CreateAsync(model);
        return Ok(result);
    }

    [HttpPost("batch")]
    public async Task<IActionResult> Create([FromBody] ICollection<DocumentTypeCreateDTO> values)
    {
        var tasks = values.Select(v => Service.CreateAsync(v)).ToArray();
        var results = await Task.WhenAll(tasks);
        return Ok(results);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] DocumentTypeUpdateDTO value)
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
