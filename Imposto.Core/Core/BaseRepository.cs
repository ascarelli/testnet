using System.Collections.Generic;
using System.Data.Entity;

namespace Imposto.Core.Core
{
    public class BaseRepository<TEntity, Ctx> : IBaseRepository<TEntity>
        where TEntity : class
        where Ctx : DbContext
    {
        public Ctx context;

        public BaseRepository(Ctx ctx)
        {
            this.context = ctx;
        }

        public virtual ProcResult Add(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }

    public class ProcResult
    {
        public int Id { get; set; }
    }
}
