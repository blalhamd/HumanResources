

using Framework.core.Models;
using Service.Comman.Enums;

namespace Service.Core.Models.ViewModels.Patient
{
    public class PatientSearchViewModel : BaseFilter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public string phone { get; set; }
    }
}
