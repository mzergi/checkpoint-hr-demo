using HrServices.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrServices.Services
{
    // todo: add automapper
    public class CrudService<TCreateModel, TUpdateModel, TReturnModel, TEntity> : ICrudService<TCreateModel, TUpdateModel, TReturnModel, TEntity>
    {
    }
}
