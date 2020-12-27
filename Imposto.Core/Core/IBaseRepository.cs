using System.Collections.Generic;

namespace Imposto.Core.Core
{
    public interface IBaseRepository<TEntity>
    {
        ProcResult Add(TEntity entity);
        IEnumerable<TEntity> GetAll();
    }
}
