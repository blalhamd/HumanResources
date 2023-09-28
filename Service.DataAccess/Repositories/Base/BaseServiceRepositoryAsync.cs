
using Framework.core.comman;
using Framework.core.IRerepositories.Base;
using Framework.DataAccess.Repositories.Base;
using Service.Core.IRepositories.Base;
using Service.DataAccess.APPDBCONTEXT;

namespace Service.DataAccess.Repositories.Base
{
    public class BaseServiceRepositoryAsync<TEntity, TPrimaryKey> : GenericRepositoryAsync<TEntity, TPrimaryKey>,
        IBaseServiceRepositoryAsync<TEntity, TPrimaryKey>

        where TEntity : class, IEntityIdentity<TPrimaryKey>
    {

        private AppDbContext _context;


        public BaseServiceRepositoryAsync(AppDbContext appDbContext, ICurrentUserService currentUserService)
            : base(appDbContext, currentUserService) { }


        public AppDbContext AppDbContext
        {
            get { return _context; }
            private set { _context = value; }
        }



    }

}
