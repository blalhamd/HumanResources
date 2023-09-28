

using Service.Comman.Enums;

namespace Service.Core.Models.DTOs
{
    public class DoctorDTO 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Specialty? specialty { get; set; }
    }
}
