

using Service.Comman.Enums;
using Service.Core.Models.DTOs;

namespace Service.Core.Models.ViewModels.Bill
{
    public class BillViewViewModel
    {
        public decimal amount { get; set; }
        public DateTime dateTime { get; set; }
        public StatusBill status { get; set; }
        public int patientId { get; set; }
        public PatientDTO patient { get; set; } = null!;
  
    }
}
