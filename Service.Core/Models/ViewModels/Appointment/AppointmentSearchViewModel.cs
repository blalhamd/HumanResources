

using Framework.core.Models;

namespace Service.Core.Models.ViewModels.Appointment
{
    public class AppointmentSearchViewModel : BaseFilter
    {
        public int Id { get; set; }
        public int patientId { get; set; }
        public int doctorId { get; set; }

    }
}
