

using Framework.core.comman;
using Service.Core.IRepositories;
using Service.DataAccess.APPDBCONTEXT;
using Service.DataAccess.Repositories.Base;
using Service.Entities.entities;

namespace Service.DataAccess.Repositories
{
    public class PrescriptionRepositoryAsync : BaseServiceRepositoryAsync<Prescription, int>, IPrescriptionRepositoryAsync
    {
        public PrescriptionRepositoryAsync(AppDbContext appDbContext, ICurrentUserService currentUserService) : base(appDbContext, currentUserService)
        {
        }
    }

}
