﻿

using Framework.core.comman;
using System.Linq.Expressions;

namespace Framework.core.IRerepositories.Base
{
    public interface IGenericRepositoryAsync<TEntity,TPrimaryKey> : IAsyncDisposable
        where TEntity : class,IEntityIdentity<TPrimaryKey>
    {

        Task<long> CountAsync();
        Task<long> CountAsync(Expression<Func<TEntity, bool>> condition);
        Task<IQueryable<TEntity>> SetIncludeNavigationAsync(IQueryable<TEntity> source, string[] includes=null!);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TPrimaryKey id);
        Task<IQueryable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> condition);
        Task<IQueryable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> condition, string[] includes = null!);
        Task<IQueryable<TEntity>> GetAsync(RepositoryRequest repositoryRequest = null!);
        Task<IQueryable<TEntity>> GetAsync(RepositoryRequestFilter<TEntity,TPrimaryKey> repositoryRequest = null!);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> condition, string[] includes=null!);
        Task<TEntity> AddEntityAsync(TEntity entity);
        Task<IList<TEntity>> AddRangeAsync(IEnumerable<TEntity> entityCollection);
        Task<TEntity> UpdateEntityAsync(TEntity entity);
        Task<IList<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entityCollection);
        Task<IList<TEntity>> UpdateRangeWithConditionAsync(Expression<Func<TEntity, bool>> condition);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(TPrimaryKey id);
        Task DeleteAsync(IEnumerable<TEntity> entityCollection);
        Task DeleteAsync(IEnumerable<TPrimaryKey> idCollection);
        Task<IQueryable<TEntity>> OrderSortAsync(IQueryable<TEntity> entityCollection,string sort);
        Task<IQueryable<TEntity>> SetPaginationAsync(IQueryable<TEntity> source,Pagination pagination);
        Task<Pagination> SetPaginationCountAsync(IQueryable<TEntity> source,Pagination pagination);


       
    }
}


