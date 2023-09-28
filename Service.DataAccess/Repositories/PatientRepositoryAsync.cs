

using Framework.core.comman;
using Service.Core.IRepositories;
using Service.DataAccess.APPDBCONTEXT;
using Service.DataAccess.Repositories.Base;
using Service.Entities.entities;

namespace Service.DataAccess.Repositories
{
    public class PatientRepositoryAsync : BaseServiceRepositoryAsync<Patient, int>, IPatientRepositoryAsync
    {
        public PatientRepositoryAsync(AppDbContext appDbContext, ICurrentUserService currentUserService) : base(appDbContext, currentUserService)
        {
        }
    }
}
    

