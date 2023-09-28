

using Service.Core.IRepositories.Base;
using Service.Entities.entities;

namespace Service.Core.IRepositories
{
    public interface IAppointmentRepositoryAsync : IBaseServiceRepositoryAsync<Appointment,int>
    {
       // for specific Actions belong to Appointment only like that
      
        // Task<Appointment> GetByName(string Name);

    }
}
