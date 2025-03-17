using Microsoft.AspNetCore.Mvc;

namespace HrApi.Abstraction;

public interface ICrudController<TEntity>
{
    public Task<IActionResult> GetPaged();

    public Task<IActionResult> Get(Guid id);
    public Task<IActionResult> Post(TEntity value);
    public Task<IActionResult> Put(TEntity value);
    public Task Delete(Guid id);
}