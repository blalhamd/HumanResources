

using System.Runtime.Serialization;

namespace Service.Comman.Enums
{
    public enum Gender : byte
    {

        [EnumMember(Value = "Male")]
        Male = 0,

        [EnumMember(Value = "Female")]
        Female,
    }
}
