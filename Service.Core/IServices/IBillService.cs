

using Framework.core.Models;
using Service.Core.IServices.Base;
using Service.Core.Models.ViewModels.Bill;
using Service.Entities.entities;

namespace Service.Core.IServices
{
    public interface IBillService : IBaseService
    {
        Task<IQueryable<BillViewModel>> GetAllAsync();
        Task<BillViewModel> GetByIdAsync(int id);
        Task<BillViewModel> AddAsync(BillViewModel billViewModel);
        Task<IList<BillViewModel>> AddRangeAsync(IEnumerable<BillViewModel> billViewModels);
        Task<Bill> UpdateAsync(UpdateBillViewModel billViewModel);
        Task<IList<Bill>> UpdateRangeAsync(IEnumerable<UpdateBillViewModel> billViewModels);
        Task DeleteAsync(int id);
        Task<GenericResult<IList<BillLightViewModel>>> Search(BillSearchViewModel searchModel);
        Task<GenericResult<IList<BillLookUpViewModel>>> SearchLookUp(BillLookUpSearchViewModel searchModel);
        Task<BillViewViewModel> Search(int id);

    }
}
