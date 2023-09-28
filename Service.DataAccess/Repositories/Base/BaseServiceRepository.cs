

using Framework.core.comman;
using Framework.core.IRerepositories.Base;
using Framework.DataAccess.Repositories.Base;
using Service.Core.IRepositories.Base;
using Service.DataAccess.APPDBCONTEXT;


namespace Service.DataAccess.Repositories.Base
{
    public class BaseServiceRepository<TEntity, TPrimaryKey> : GenericRepository<TEntity, TPrimaryKey>,
        IBaseServiceRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntityIdentity<TPrimaryKey>
    {

        private AppDbContext _context;

        public BaseServiceRepository(AppDbContext context, ICurrentUserService currentUserService) : base(context, currentUserService) { }

        public AppDbContext AppDbContext
        {
            get { return _context; }
            set { _context = value; }
        }
    }

}
