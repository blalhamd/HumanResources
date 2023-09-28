

using Framework.comman.Enums;
using Framework.comman.interfaces;
using Framework.core.comman;
using Framework.core.IRerepositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Framework.DataAccess.Repositories.Base
{
    public class GenericRepositoryAsync<TEntity, TPrimaryKey> : IAsyncDisposable,IDisposable,
                  IGenericRepositoryAsync<TEntity, TPrimaryKey> where TEntity : class, IEntityIdentity<TPrimaryKey>
    {

        private DbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private DbSet<TEntity> Entities { get; set; }
        public GenericRepositoryAsync(DbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
            Entities = _context.Set<TEntity>();

        }

        protected DbContext Context
        {
            get => _context;
            set
            {
                _context = value;
            }
        }

        public void Dispose()
        {
            Context.DisposeAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await Context.DisposeAsync();
        }

        public async Task<long> CountAsync()
        {
            return await Entities.CountAsync();
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await Entities.LongCountAsync(condition);
        }


        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> condition, string[] includes = null)
        {
            var query = Entities.AsQueryable();
            TEntity entity = null!;

            if (includes != null)
            {

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            entity = await query.FirstOrDefaultAsync(condition);

            return entity;
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                var query = Entities.AsNoTracking().AsQueryable();

                return query;
            });
        }

        public async Task<IQueryable<TEntity>> GetAsync(RepositoryRequest repositoryRequest = null)
        {
            return await Task.Run(async() =>
            {
                var query = Entities.AsQueryable();

                if (repositoryRequest != null)
                {
                    if (repositoryRequest.includes != null)
                    {
                        foreach (var item in repositoryRequest.includes)
                        {
                            query = query.Include(item);
                        }
                    }

                    if (string.IsNullOrEmpty(repositoryRequest.Sorting) && repositoryRequest.Sorting != "null")
                    {
                        query = await OrderSortAsync(query, repositoryRequest.Sorting);
                    }

                    if (repositoryRequest.order.HasValue)
                    {
                        if (repositoryRequest.order.Value == Order.Ascending)
                            query = query.OrderBy(x => x.Id);
                        else
                            query = query.OrderByDescending(x => x.Id);
                    }

                    query = query.Skip(repositoryRequest.pagination.PageNumber.Value * repositoryRequest.pagination.PageSize.Value - 1)
                                 .Take(repositoryRequest.pagination.PageSize.Value);
                }

                return query;
            }); 
        }

        public async Task<IQueryable<TEntity>> GetAsync(RepositoryRequestFilter<TEntity, TPrimaryKey> repositoryRequest = null)
        {
            return await Task.Run(async () =>
            {
                var query = Entities.AsQueryable();

                if (repositoryRequest != null)
                {
                    if (repositoryRequest.includes != null)
                    {
                        foreach (var item in repositoryRequest.includes)
                        {
                            query = query.Include(item);
                        }
                    }

                    if (repositoryRequest.condition != null)
                    {
                        query = query.Where(repositoryRequest.condition);
                    }

                    if (string.IsNullOrEmpty(repositoryRequest.Sorting) && repositoryRequest.Sorting != "null")
                    {
                        query = await OrderSortAsync(query, repositoryRequest.Sorting);
                    }

                    if (repositoryRequest.order.HasValue)
                    {
                        if (repositoryRequest.order.Value == Order.Ascending)
                            query = query.OrderBy(x => x.Id);
                        else
                            query = query.OrderByDescending(x => x.Id);
                    }

                    query = query.Skip(repositoryRequest.pagination.PageNumber.Value * repositoryRequest.pagination.PageSize.Value -1 )
                                 .Take(repositoryRequest.pagination.PageSize.Value);
                }

                return query;
            });
        }

        public async Task<IQueryable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await Task.Run(() =>
            {
                var query = Entities.AsQueryable();

                if (condition != null)
                {
                    query = query.Where(condition);
                }

                return query;
            });
        }

        public async Task<IQueryable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> condition, string[] includes = null)
        {
            return await Task.Run(() =>
            {
                var query = Entities.Where(condition);

                if (query != null && includes != null)
                {
                    foreach (var item in includes)
                    {
                        query = query.Include(item);
                    }
                }

                return query.AsQueryable();
            });
        }

  
        public async Task<TEntity> GetByIdAsync(TPrimaryKey id)
        {
            return await Entities.FindAsync(id);
        }

        public async Task<IQueryable<TEntity>> OrderSortAsync(IQueryable<TEntity> entityCollection, string sort)
        {
            if(entityCollection != null)
            {
                entityCollection = await OrderSortAsync(entityCollection, sort);
            }

            return entityCollection;
        }

        public async Task<IQueryable<TEntity>> SetIncludeNavigationAsync(IQueryable<TEntity> source, string[] includes = null)
        {
            return await Task.Run(() => 
            {
                if (source != null && includes != null)
                {
                    foreach (var item in includes)
                    {
                        source = source.Include(item);
                    }
                }

                return source.AsQueryable();
            });
           
        }

        public async Task<IQueryable<TEntity>> SetPaginationAsync(IQueryable<TEntity> source, Pagination pagination)
        {

            return await Task.Run(() =>
            {
                if (source != null && pagination != null && pagination.PageSize != null && pagination.PageNumber!=null)
                {
                    source = source.Skip(pagination.PageNumber.Value * pagination.PageSize.Value)
                                   .Take(pagination.PageSize.Value);
                }

                return source.AsQueryable();
            }
            );
           
        }

        public async Task<Pagination> SetPaginationCountAsync(IQueryable<TEntity> source, Pagination pagination)
        {

            if(source != null && pagination != null && pagination.GetTotalCount)
            {
                pagination.TotalCount= await source.CountAsync();
            }

            return pagination;
        }

        public async Task<TEntity> AddEntityAsync(TEntity entity)
        {
            if (entity is ICreationTimeSignature)
            {
                var creationTimeSignature = (ICreationTimeSignature)entity;
                creationTimeSignature.CreationTime = DateTime.Now;
            }

            if (entity is IEntityCreatedUserSignature)
            {
                var entityCreatedUserSignature = (IEntityCreatedUserSignature)entity;
                entityCreatedUserSignature.CreatedByUserId = _currentUserService.CurrentUserId;
            }

            await Entities.AddAsync(entity);

            return entity;
        }

        public async Task<IList<TEntity>> AddRangeAsync(IEnumerable<TEntity> entityCollection)
        {
            List<TEntity> entities = new List<TEntity>();

            foreach (var entity in entityCollection)
            {
                if (entity is ICreationTimeSignature)
                {
                    var creationTimeSignature = (ICreationTimeSignature)entity;
                    creationTimeSignature.CreationTime = DateTime.Now;
                }

                if (entity is IEntityCreatedUserSignature)
                {
                    var entityCreatedUserSignature = (IEntityCreatedUserSignature)entity;
                    entityCreatedUserSignature.CreatedByUserId = _currentUserService.CurrentUserId;
                }

                if(entity is IDateTimeSignature)
                {
                    var dateTimeSignature= (IDateTimeSignature)entity;
                    
                    if(dateTimeSignature.FirstModificationDate.HasValue==false)
                        dateTimeSignature.FirstModificationDate = DateTime.Now;
                    else
                        dateTimeSignature.LastModificationDate = DateTime.Now;
                }

                if(entity is IEntityUserSignature)
                {
                    var entityUserSignature= (IEntityUserSignature)entity;

                    if(entityUserSignature.FirstModifiedByUserId.HasValue==false)
                       entityUserSignature.FirstModifiedByUserId= _currentUserService.CurrentUserId;
                    else
                        entityUserSignature.LastModifiedByUserId = _currentUserService.CurrentUserId;
                }

                entities.Add(entity);
            }

            await Entities.AddRangeAsync(entities);

            return entities;
        }

        public async Task<TEntity> UpdateEntityAsync(TEntity entity)
        {

            return await Task.Run(() =>
            {
                if (entity is IDateTimeSignature)
                {
                    var dateTimeSignature = (IDateTimeSignature)entity;

                    if (dateTimeSignature.FirstModificationDate.HasValue == false)
                        dateTimeSignature.FirstModificationDate = DateTime.Now;

                    else
                        dateTimeSignature.LastModificationDate = DateTime.Now;
                }

                if (entity is IEntityCreatedUserSignature)
                {
                    var entityCreatedUserSignature = (IEntityCreatedUserSignature)entity;
                    entityCreatedUserSignature.CreatedByUserId = _currentUserService.CurrentUserId;
                }

                Entities.Update(entity);

                return entity;
            });
        }

        public async Task<IList<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entityCollection)
        {
            return await Task.Run(() =>
            {
                List<TEntity> entities = new List<TEntity>();

                if (entityCollection != null)
                {
                    foreach (var entity in entityCollection)
                    {
                        if (entity is IDateTimeSignature)
                        {
                            var dateTimeSignature = (IDateTimeSignature)entity;

                            if (dateTimeSignature.FirstModificationDate.HasValue == false)
                                dateTimeSignature.FirstModificationDate = DateTime.Now;

                            else
                                dateTimeSignature.LastModificationDate = DateTime.Now;
                        }

                        entities.Add(entity);
                    }
                }

                Entities.UpdateRange(entities);

                return entities;
            });
        }

        public async Task<IList<TEntity>> UpdateRangeWithConditionAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await Task.Run(() =>
            {
                var query = Entities.Where(condition);
                List<TEntity> entities = new List<TEntity>();

                if (query != null)
                {
                    foreach (var entity in query)
                    {
                        if (entity is IDateTimeSignature)
                        {
                            var dateTimeSignature = (IDateTimeSignature)entity;

                            if (dateTimeSignature.FirstModificationDate.HasValue == false)
                                dateTimeSignature.FirstModificationDate = DateTime.Now;

                            else
                                dateTimeSignature.LastModificationDate = DateTime.Now;
                        }

                        entities.Add(entity);
                    }
                }

                Entities.UpdateRange(entities);

                return entities;
            });
        }


        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                if (entity is IDeletionSignature)
                {
                    var deletionSignature = (IDeletionSignature)entity;

                    if (deletionSignature.MustDeletedPhysical == true)
                    {
                        Entities.Remove(entity);
                        Context.SaveChangesAsync();
                    }
                    else
                    {
                        deletionSignature.DeletionDate = DateTime.Now;
                        deletionSignature.IsDeleted = true;
                        deletionSignature.DeletedByUserId = _currentUserService.CurrentUserId;

                        Entities.Update(entity);
                    }
                }
                else
                {
                    Entities.Remove(entity);
                }
            });
        }

        public async Task DeleteAsync(TPrimaryKey id)
        {
            var query = await Entities.FindAsync(id);

            await DeleteAsync(query);
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entityCollection)
        {
            foreach (var entity in entityCollection)
            {
                await DeleteAsync(entity);
            }
        }

        public async Task DeleteAsync(IEnumerable<TPrimaryKey> idCollection)
        {
            foreach (var id in idCollection)
            {
                var entity = await Entities.FindAsync(id);

                await DeleteAsync(entity);
            }
        }

 
    }
}
