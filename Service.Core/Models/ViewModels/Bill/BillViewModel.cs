

using Service.Comman.Enums;

namespace Service.Core.Models.ViewModels.Bill
{
    public class BillViewModel
    {
        public decimal amount { get; set; }
        public DateTime dateTime { get; set; }
        public StatusBill status { get; set; }
        public int patientId { get; set; }
    }
}
