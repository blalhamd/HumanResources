

using System.Runtime.Serialization;

namespace Service.Comman.Enums
{
    public enum StatusAppointment
    {
        [EnumMember(Value = "pending")]
        pending,

        [EnumMember(Value = "Confirmed")]
        Confirmed,

        [EnumMember(Value = "cancelled")]
        cancelled
    }
}
