

using Framework.core.IRepositories;
using Service.DataAccess.APPDBCONTEXT;

namespace Service.DataAccess.Repositories
{
    public class UnitOfWork : IunitOfWork
    {
        private AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Commit()
        {
           var save = _dbContext.SaveChanges();

            return save;
        }
    }
}
