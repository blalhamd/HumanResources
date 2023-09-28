

using Service.Comman.Enums;

namespace Service.Core.Models.ViewModels.Appointment
{
    public class AppointmentLookUpViewModel // that will return and use for dropdown by select
    {
        public int Id { get; set; }
        public DateTime dateTime { get; set; }
        public StatusAppointment status { get; set; }
        public int patientId { get; set; }
        public int doctorId { get; set; }
    }
}
