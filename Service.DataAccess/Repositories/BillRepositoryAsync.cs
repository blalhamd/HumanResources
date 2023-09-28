

using Framework.core.comman;
using Service.Core.IRepositories;
using Service.DataAccess.APPDBCONTEXT;
using Service.DataAccess.Repositories.Base;
using Service.Entities.entities;

namespace Service.DataAccess.Repositories
{
    public class BillRepositoryAsync : BaseServiceRepositoryAsync<Bill, int>, IBillRepositoryAsync
    {
        public BillRepositoryAsync(AppDbContext appDbContext, ICurrentUserService currentUserService) : base(appDbContext, currentUserService)
        {
        }
    }
  
}
