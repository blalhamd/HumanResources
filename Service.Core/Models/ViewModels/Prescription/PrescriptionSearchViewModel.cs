

using Framework.core.Models;

namespace Service.Core.Models.ViewModels.Prescription
{
    public class PrescriptionSearchViewModel : BaseFilter
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
    }
}
