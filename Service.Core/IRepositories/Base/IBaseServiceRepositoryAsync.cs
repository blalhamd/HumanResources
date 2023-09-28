
using Framework.core.IRerepositories.Base;

namespace Service.Core.IRepositories.Base
{
    public interface IBaseServiceRepositoryAsync<TEntity,TPrimaryKey> : IGenericRepositoryAsync<TEntity,TPrimaryKey>
                     where TEntity : class,IEntityIdentity<TPrimaryKey>
    {
    }
}
