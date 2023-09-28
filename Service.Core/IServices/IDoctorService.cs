
using Framework.core.Models;
using Service.Core.IServices.Base;
using Service.Core.Models.ViewModels.Doctor;
using Service.Entities.entities;

namespace Service.Core.IServices
{
    public interface IDoctorService : IBaseService
    {
        Task<IQueryable<DoctorViewModel>> GetAllAsync();
        Task<DoctorViewModel> GetByIdAsync(int id);
        Task<DoctorViewModel> AddAsync(DoctorViewModel doctorViewModel);
        Task<IList<DoctorViewModel>> AddRangeAsync(IEnumerable<DoctorViewModel> doctorViewModel);
        Task<Doctor> UpdateAsync(UpdateDoctorViewModel doctorViewModel);
        Task<IList<Doctor>> UpdateRangeAsync(IEnumerable<UpdateDoctorViewModel> doctorViewModel);
        Task DeleteAsync(int id);
        Task<GenericResult<IList<DoctorLightViewModel>>> Search(DoctorSearchViewModel searchModel);
        Task<GenericResult<IList<DoctorLookUpViewModel>>> SearchLookUp(DoctorLookUpSearchViewModel searchModel);
        Task<DoctorViewViewModel> Search(int id);
    }
}
