

using Service.Comman.Enums;

namespace Service.Core.Models.ViewModels.Appointment
{
    public class UpdateAppointMentViewModel // I customized it for Updating because I want to add Id that by it will hold
                                            // entity need to update first.
{
        public int Id { get; set; }
        public DateTime dateTime { get; set; }
        public StatusAppointment status { get; set; }
        public int patientId { get; set; }
        public int doctorId { get; set; }
    }
}
