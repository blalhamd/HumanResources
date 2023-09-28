

using Service.Comman.Enums;
using Service.Core.Models.DTOs;

namespace Service.Core.Models.ViewModels.Doctor
{
    public class UpdateDoctorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Specialty? specialty { get; set; }
        public string license { get; set; }
        public string contact { get; set; }
    }
}
