

using Service.Comman.Enums;

namespace Service.Core.Models.ViewModels.Bill
{
    public class BillLookUpViewModel  // that will return and use for dropdown by select
    {
        public int Id { get; set; }
        public decimal amount { get; set; }
        public DateTime dateTime { get; set; }
        public StatusBill status { get; set; }
        public int patientId { get; set; }
       
    }
}
