using HrServices.DTOs.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HrApi.Abstraction;

public interface ICrudController<TEntity, TCreateEntity, TUpdateEntity>
{
    public Task<IActionResult> GetPaged(PageFilters pageFilters);

    public Task<IActionResult> Get(Guid id);
    public Task<IActionResult> Create(TCreateEntity value);
    public Task<IActionResult> Update(Guid id, TUpdateEntity value);
    public Task Delete(Guid id);
}