

using Framework.core.Models;

namespace Service.Core.Models.ViewModels.Appointment
{
    public class AppointmentLookUpSearchViewModel : BaseFilter  // for filteration by Name,Id...etc
    {
        public int Id { get; set; }
        public int patientId { get; set; }
        public int doctorId { get; set; }
        public bool paginaed { get; set; }
        public bool Sorted { get; set; }
        
    }
}
