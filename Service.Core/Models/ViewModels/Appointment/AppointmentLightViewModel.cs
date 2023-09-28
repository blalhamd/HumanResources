

using Service.Comman.Enums;

namespace Service.Core.Models.ViewModels.Appointment
{
    public class AppointmentLightViewModel // for drop down selection to lightweight the results
    {
        public int Id { get; set; }
        public DateTime dateTime { get; set; }
        public StatusAppointment status { get; set; }
        public int patientId { get; set; }
        public int doctorId { get; set; }
        
    }
}
