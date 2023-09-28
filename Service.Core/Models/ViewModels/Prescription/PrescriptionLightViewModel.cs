

using Service.Core.Models.DTOs;

namespace Service.Core.Models.ViewModels.Prescription
{
    public class PrescriptionLightViewModel
    {
        public int Id { get; set; }
        public string medication { get; set; }
        public string? dosage { get; set; }
        public int frequency { get; set; }   
        public string duration { get; set; } 
        public int AppointmentId { get; set; }
        public AppointmentDTO appointment { get; set; } = null!;
    }
}
