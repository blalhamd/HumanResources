

using Framework.core.IRerepositories.Base;

namespace Service.Core.IRepositories.Base
{
    public interface IBaseServiceRepository<TEntity,TPrimaryKey> : IGenericRepository<TEntity,TPrimaryKey> 
        where TEntity : class,IEntityIdentity<TPrimaryKey>
    {
    }
}
