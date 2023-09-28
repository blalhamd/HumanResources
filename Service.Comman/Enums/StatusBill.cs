

using System.Runtime.Serialization;

namespace Service.Comman.Enums
{
    public enum StatusBill 
    {

        [EnumMember(Value = "Paid")]
        Paid,

        [EnumMember(Value = "Unpaid")]
        Unpaid,

        [EnumMember(Value = "overdue")]
        overdue,
    }
}
