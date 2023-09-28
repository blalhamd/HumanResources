
using Framework.core.Models;
using Service.Core.IServices.Base;
using Service.Core.Models.ViewModels.Patient;
using Service.Entities.entities;

namespace Service.Core.IServices
{
    public interface IPatientService: IBaseService
    {
        Task<IQueryable<PatientViewModel>> GetAllAsync();
        Task<PatientViewModel> GetByIdAsync(int id);
        Task<PatientViewModel> AddAsync(PatientViewModel patientViewModel);
        Task<IList<PatientViewModel>> AddRangeAsync(IEnumerable<PatientViewModel> patientViewModels);
        Task<Patient> UpdateAsync(UpdatePatientViewModel patientViewModel);
        Task<IList<Patient>> UpdateRangeAsync(IEnumerable<UpdatePatientViewModel> patientViewModel);
        Task DeleteAsync(int id);
        Task<GenericResult<IList<PatientLightViewModel>>> Search(PatientSearchViewModel searchModel);
        Task<GenericResult<IList<PatientLookUpViewModel>>> SearchLookUp(PatientLookUpSearchViewModel searchModel);
        Task<PatientViewViewModel> Search(int id);
    }
}
