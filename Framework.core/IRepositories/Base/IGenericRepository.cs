

using Framework.core.comman;
using System.Linq.Expressions;

namespace Framework.core.IRerepositories.Base
{
    public interface IGenericRepository<TEntity,TPrimaryKey>
           where TEntity : class, IEntityIdentity<TPrimaryKey>
    {

        long count();  // to calculate count
        long count(Expression<Func<TEntity, bool>> condition); // to calc count by specific condition
        TEntity GetEntityById(TPrimaryKey id);  // to get Entity by Id
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> condition, string[] inculde = null!); // will return the first item will match condition
        IQueryable<TEntity> GetAll();  // to get allEntities    IList<TEntity> GetAll();  // to get allEntities
        IQueryable<TEntity> GetAll(string[] includes = null!);  // to get All Entites with include
        IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> condition);  // to get All Entites by condition
        IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> condition, string[] includes = null!);  // to get All Entites by condition and include
        IQueryable<TEntity> Get(RepositoryRequest repositoryRequest);  // will contain pagination,sorting,includes,order
        IQueryable<TEntity> GetWithFilteration(RepositoryRequestFilter<TEntity,TPrimaryKey> repositoryRequest); // will contain pagination,sorting,includes,order,condition
        IQueryable<TEntity> SetIncludeNavigationsList(IQueryable<TEntity> source, string[] includes = null!);  // use to include related entities in the query result.
        IQueryable<TEntity> SetSortOrder(IQueryable<TEntity> source, string SortOrder); // is used to sort the query result based on a specified sort order.
        IQueryable<TEntity> SetPagination(IQueryable<TEntity> source, Pagination pagination);
        Pagination SetPaginationCount(IQueryable<TEntity> source, Pagination pagination);
        TEntity AddEntity(TEntity entity);  // to add entity
        IList<TEntity> AddRange(IEnumerable<TEntity> entitiyCollection); // to add list of entities
        TEntity UpdateEntity(TEntity entity); // to Update entity
        IList<TEntity> UpdateRange(IEnumerable<TEntity> entityCollection);  // to update list of entities
        IList<TEntity> UpdateWithCondition(Expression<Func<TEntity, bool>> condition);  // to update who match condition
        void DeleteEntity(TEntity entity);  // to delete entity
        void DeleteEntity(TPrimaryKey id);  // to delete entity by id
        void DeleteRange(IEnumerable<TEntity> entityCollection);  // to delete collection
        void DeleteRange(IEnumerable<TPrimaryKey> IdCollection);  // to delete collection by ids
        void DeleteByCondition(Expression<Func<TEntity, bool>> condition); // to delete by condition
        



    }
}



/*
    example illustrate what is that?
   
    IQueryable<TEntity> SetIncludedNavigationsList(IQueryable<TEntity> source, IEnumerable<string> list);  
   
     
    public IQueryable<TEntity> GetEntities()
    {
        var query = _context.Set<TEntity>();  // source
        query = SetIncludedNavigationsList(query, new List<string> { "RelatedEntity1", "RelatedEntity2" });
        return query;
    }
 
 */

