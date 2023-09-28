

using Framework.core.Models;

namespace Service.Core.Models.ViewModels.Bill
{
    public class BillLookUpSearchViewModel : BaseFilter // for filteration by Name,Id...etc
    {
        public int Id { get; set; }
        public int patientId { get; set; }

    }
}
