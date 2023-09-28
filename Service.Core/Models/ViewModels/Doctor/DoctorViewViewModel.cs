

using Service.Comman.Enums;

namespace Service.Core.Models.ViewModels.Doctor
{
    public class DoctorViewViewModel
    {

        public string Name { get; set; }
        public Specialty? specialty { get; set; }
        public string license { get; set; }
        public string contact { get; set; }
    

    }
}
