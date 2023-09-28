

using Framework.core.Models;
using Service.Core.IServices.Base;
using Service.Core.Models.ViewModels.Appointment;
using Service.Entities.entities;

namespace Service.Core.IServices
{
    public interface IAppointmentService : IBaseService
    {
        Task<IQueryable<AppointmentViewModel>> GetAllAsync();
        Task<AppointmentViewModel> GetByIdAsync(int id);
        Task<AppointmentViewModel> AddAsync(AppointmentViewModel appointmentViewModel);
        Task<IList<AppointmentViewModel>> AddRangeAsync(IEnumerable<AppointmentViewModel> appointmentViewModel);
        Task<Appointment> UpdateAsync(UpdateAppointMentViewModel appointmentViewModel);
        Task<IList<Appointment>> UpdateRangeAsync(IEnumerable<UpdateAppointMentViewModel> appointmentViewModel);
        Task DeleteAsync(int id);
        Task<GenericResult<IList<AppointmentLightViewModel>>> Search(AppointmentSearchViewModel searchModel);
        Task<GenericResult<IList<AppointmentLookUpViewModel>>> SearchLookUp(AppointmentLookUpSearchViewModel searchModel);
      //  Task<IList<AppointmentViewViewModel>> Search(AppointmentLookUpSearchViewModel searchModel);
        Task<AppointmentViewViewModel> Search(int id);

    }
}
