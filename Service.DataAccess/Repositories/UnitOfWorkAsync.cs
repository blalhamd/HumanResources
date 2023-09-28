

using Framework.core.IRepositories;
using Service.DataAccess.APPDBCONTEXT;

namespace Service.DataAccess.Repositories
{
    public class UnitOfWorkAsync : IunitOfWorkAsync
    {
        private AppDbContext _context;

        public UnitOfWorkAsync(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> commitAsync()
        {
           var save = await _context.SaveChangesAsync();

            return save;
        }
    }
}
