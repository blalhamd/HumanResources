

using Service.Comman.Enums;

namespace Service.Core.Models.ViewModels.Appointment
{
    public class AppointmentViewModel  // use for Adding , Note I did't add Id because I made it Identity.
    {
        public int patientId { get; set; }
        public int doctorId { get; set; }
        public StatusAppointment status { get; set; }
        public DateTime dateTime { get; set; }
       

    }
}
