

using Service.Comman.Enums;
using Service.Core.Models.DTOs;

namespace Service.Core.Models.ViewModels.Appointment
{
    public class AppointmentViewViewModel
    {
        public DateTime dateTime { get; set; }
        public StatusAppointment status { get; set; }
        public int patientId { get; set; }
        public int doctorId { get; set; }

    }
}
