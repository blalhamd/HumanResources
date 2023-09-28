


using Framework.comman.Enums;
using Framework.comman.interfaces;
using Framework.core.comman;
using Framework.core.IRerepositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Framework.DataAccess.Repositories.Base
{
    public class GenericRepository<TEntity, TPrimaryKey> : IDisposable,
                  IGenericRepository<TEntity, TPrimaryKey>
                  where TEntity : class, IEntityIdentity<TPrimaryKey>
    {

        private DbContext _context;
        private readonly ICurrentUserService _currentUserService;
        protected DbSet<TEntity> Entities { get; set; }

        public GenericRepository(DbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
            Entities = _context.Set<TEntity>();

        }

        protected DbContext Context
        {
            get { return _context; }
            set 
            { 
                _context = value;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }


        public long count()
        {
            return Entities.Count();
        }


        public long count(Expression<Func<TEntity, bool>> condition)
        {
            return Entities.LongCount(condition);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Entities.AsNoTracking().AsQueryable();
        }

        public IQueryable<TEntity> GetAll(string[] includes = null!)
        {
            var query= Entities.AsNoTracking().AsQueryable();

            if(includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }

            return query;
        }

        public IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> condition)
        {
            var query = Entities.Where(condition).AsNoTracking();

            return query;
        }

        public IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> condition, string[] includes = null)
        {
            var query = Entities.Where(condition);

            foreach (var item in includes)
            {
                query = query.Include(item);
            }

            return query;
        }

        public TEntity GetEntityById(TPrimaryKey id)
        {
            return Entities.Find(id);
        }


        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> condition, string[] inculde = null!)
        {
            var query = Entities.AsQueryable();
            TEntity result = null;

            if (inculde != null)
            {
                foreach (var item in inculde)
                {
                    query = query.Include(item);
                }
            }

            if(condition != null)
            {
                result = query.FirstOrDefault(condition);
            }


            return result;
        }

        public IQueryable<TEntity> Get(RepositoryRequest repositoryRequest)
        {

           var result = Entities.AsQueryable();

            if(repositoryRequest != null)
            {
                if(repositoryRequest.includes != null)
                {
                    foreach (var item in repositoryRequest.includes)
                    {
                        result = SetIncludeNavigationsList(result, new[] { item });
                    }
                }

                repositoryRequest.pagination = SetPaginationCount(result, repositoryRequest.pagination);

                if (string.IsNullOrEmpty(repositoryRequest.Sorting)==false && repositoryRequest.Sorting!="null")
                {
                    result = SetSortOrder(result, repositoryRequest.Sorting);
                }

                if (repositoryRequest.order.HasValue)
                {
                    if (repositoryRequest.order.Value == Order.Ascending)
                        result = result.OrderBy(x => x.Id);
                    else
                        result = result.OrderByDescending(x => x.Id);
                }

                result = SetPagination(result, repositoryRequest.pagination);
            }

            return result;

        }

        public IQueryable<TEntity> GetWithFilteration(RepositoryRequestFilter<TEntity, TPrimaryKey> repositoryRequest)
        {
           var result = Entities.AsQueryable();

            if(repositoryRequest != null)
            {
                foreach (var item in repositoryRequest.includes)
                {
                    result = SetIncludeNavigationsList(result, new[] {item});
                }

                if (repositoryRequest.condition != null)
                {
                    result = result.Where(repositoryRequest.condition);
                }

                repositoryRequest.pagination = SetPaginationCount(result, repositoryRequest.pagination);

                if (string.IsNullOrEmpty(repositoryRequest.Sorting) == false && repositoryRequest.Sorting != "null")
                {
                    result = SetSortOrder(result, repositoryRequest.Sorting);
                }

                else if (repositoryRequest.order.HasValue)
                {
                    if (repositoryRequest.order.Value == Order.Ascending)
                        result = result.OrderBy(x => x.Id);
                    else
                        result = result.OrderByDescending(x => x.Id);

                }

                result= SetPagination(result, repositoryRequest.pagination);

            }

            return result;
        }

        public TEntity AddEntity(TEntity entity)
        {

            if (entity is ICreationTimeSignature)
            {
                ICreationTimeSignature dataTimeSignature = (ICreationTimeSignature)entity; // here case entity to ICreationTimeSignature
                dataTimeSignature.CreationTime = DateTime.Now;
            }

            if(entity is IEntityCreatedUserSignature)
            {
                IEntityCreatedUserSignature createdByUserId = (IEntityCreatedUserSignature)entity; // here cast entity to IEntityCreatedUserSignature
                createdByUserId.CreatedByUserId = _currentUserService.CurrentUserId;
            }

            Entities.Add(entity);

            return entity;
        }

        public IList<TEntity> AddRange(IEnumerable<TEntity> entitiyCollection)
        {
            IList<TEntity> entities = new List<TEntity>();

            foreach (var entity in entitiyCollection)
            {
                if (entity is ICreationTimeSignature)
                {
                    ICreationTimeSignature TimeCreation= (ICreationTimeSignature)entity;
                    TimeCreation.CreationTime = DateTime.Now;
                }

                if(entity is IEntityCreatedUserSignature)
                {
                    IEntityCreatedUserSignature userSignature= (IEntityCreatedUserSignature)entity;
                    userSignature.CreatedByUserId = _currentUserService.CurrentUserId;
                }

                entities.Add(entity);
            }

            Entities.AddRange(entities);

            return entities;
        }


       
        public IQueryable<TEntity> SetIncludeNavigationsList(IQueryable<TEntity> source, string[] includes = null)
        {
             if(source != null && includes != null)
             {
                foreach (var item in includes)
                {
                    source = source.Include(item);
                }
             }

            return source;
        }             
        

        public IQueryable<TEntity> SetPagination(IQueryable<TEntity> source, Pagination pagination)
        {
            if(pagination != null && source != null && pagination.PageSize.HasValue && pagination.PageNumber.HasValue)
            {
                source = source.Skip(pagination.PageNumber.Value * pagination.PageSize.Value -1 )
                                  .Take(pagination.PageSize.Value);
            }

            return source;
        }

        public Pagination SetPaginationCount(IQueryable<TEntity> source, Pagination pagination)
        {
            if(source != null && pagination != null && pagination.GetTotalCount)
            {
                pagination.TotalCount = source.Count();
            }

            return pagination;
        }

        public IQueryable<TEntity> SetSortOrder(IQueryable<TEntity> source, string SortOrder)
        {

            if (source != null && string.IsNullOrEmpty(SortOrder) == false && SortOrder != "null")
            {
                if (SortOrder.ToLower() == Order.Ascending.ToString().ToLower())
                    source = source.OrderBy(x => x.Id);
                else
                    source = source.OrderByDescending(x => x.Id);
            }

            return source;
        }

        public TEntity UpdateEntity(TEntity entity)
        {
           
            if (entity is IDateTimeSignature)
            {
                var dateTimeSignature = (IDateTimeSignature)entity;

                if (dateTimeSignature.FirstModificationDate.HasValue==false)
                   
                    dateTimeSignature.FirstModificationDate = DateTime.Now;

                else
                    dateTimeSignature.LastModificationDate = DateTime.Now;
            }

            if (entity is IEntityUserSignature)
            {
                var entityUserSignature= (IEntityUserSignature)entity;

                if (entityUserSignature.FirstModifiedByUserId.HasValue == false)
                    entityUserSignature.CreatedByUserId = _currentUserService.CurrentUserId;

                else
                    entityUserSignature.LastModifiedByUserId = _currentUserService.CurrentUserId;
            }

            Entities.Update(entity);

            return entity;
        }

        public IList<TEntity> UpdateRange(IEnumerable<TEntity> entityCollection)
        {
            IList<TEntity> entities = new List<TEntity>();

            foreach (var item in entityCollection)
            {
                if(item is IDateTimeSignature)
                {
                    var dateTimeSignature= (IDateTimeSignature)item;

                    if(dateTimeSignature.FirstModificationDate.HasValue==false)
                    {
                        dateTimeSignature.FirstModificationDate = DateTime.Now;
                    }
                    else
                    {
                        dateTimeSignature.LastModificationDate= DateTime.Now;
                    }
                }

                if(item is IEntityUserSignature)
                {
                    var entityUserSignature=(IEntityUserSignature)item;
                    
                    if (entityUserSignature.FirstModifiedByUserId.HasValue == false)
                    {
                        entityUserSignature.FirstModifiedByUserId = _currentUserService.CurrentUserId;
                    }
                    else
                    {
                        entityUserSignature.LastModifiedByUserId = _currentUserService.CurrentUserId;
                    }
                }

                entities.Add(item);

            }

            Entities.UpdateRange(entities);

            return entities;
        }

        public IList<TEntity> UpdateWithCondition(Expression<Func<TEntity, bool>> condition)
        {
            var query = Entities.Where(condition);
            IList<TEntity> entities = query.ToList();

            foreach (var entity in query)
            {
                if (entity is IDateTimeSignature)
                {
                    var dateTimeSignature = (IDateTimeSignature)entity;

                    if(dateTimeSignature.FirstModificationDate.HasValue==false)
                        dateTimeSignature.FirstModificationDate= DateTime.Now;

                    else
                        dateTimeSignature.LastModificationDate= DateTime.Now;

                }

                if(entity is IEntityUserSignature)
                {
                    var userSignature=(IEntityUserSignature)entity;

                    if(userSignature.FirstModifiedByUserId.HasValue==false)
                        userSignature.FirstModifiedByUserId=_currentUserService.CurrentUserId;
                    else
                        userSignature.LastModifiedByUserId= _currentUserService.CurrentUserId;
                }

                entities.Add(entity);

            }

            Entities.UpdateRange(entities);

            return entities;
        }

        public void DeleteByCondition(Expression<Func<TEntity, bool>> condition)
        {
            var query = Entities.Where(condition);

            foreach (var item in query)
            {
                Entities.Remove(item);
            }
        }

        public void DeleteEntity(TEntity entity)
        {

            if (entity is IDeletionSignature)
            {
                var deletionSignature = (IDeletionSignature)entity;

                if (deletionSignature.MustDeletedPhysical == true)
                {
                    Entities.Remove(entity);
                }
                else
                {
                    deletionSignature.IsDeleted = true;
                    deletionSignature.DeletionDate = DateTime.Now;
                    deletionSignature.DeletedByUserId = _currentUserService.CurrentUserId;

                    Entities.Update(entity);
                }
            }

            else
            {
                Entities.Remove(entity);
            }

        }

        public void DeleteEntity(TPrimaryKey id)
        {
            var query = Entities.Find(id);

            DeleteEntity(query);
        }

        public void DeleteRange(IEnumerable<TEntity> entityCollection)
        {
            foreach (var entity in entityCollection)
            {
                DeleteEntity(entity);
            }
        }

        public void DeleteRange(IEnumerable<TPrimaryKey> IdCollection)
        {

            foreach (var id in IdCollection)
            {
                var entity = Entities.Find(id);

                DeleteEntity(entity);
            }
        }




    }
}
