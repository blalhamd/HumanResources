

using Framework.core.Models;

namespace Service.Core.Models.ViewModels.Bill
{
    public class BillSearchViewModel : BaseFilter
    {
        public int Id { get; set; }
        public int patientId { get; set; }
    }
}
