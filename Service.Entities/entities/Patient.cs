

using Framework.comman.interfaces;
using Framework.core.IRerepositories.Base;
using Service.Comman.Enums;

namespace Service.Entities.entities
{
    public class Patient : IEntityIdentity<int>, ICreationTimeSignature, IDeletionSignature, IDateTimeSignature, IEntityUserSignature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public Gender gender { get; set; }
        public string? address { get; set; }
        public string phone { get; set; }
        public bool? HasInsurance { get; set; }
        public ICollection<Bill> Bills { get; set; } = new List<Bill>();
        public ICollection<Appointment> appointments { get; set; } = new List<Appointment>();
        public DateTime CreationTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionDate { get; set; }
        public long? DeletedByUserId { get; set; }
        public bool? MustDeletedPhysical { get; set; }
        public DateTime? FirstModificationDate { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public long? CreatedByUserId { get; set; }
        public long? FirstModifiedByUserId { get; set; }
        public long? LastModifiedByUserId { get; set; }
    }
}
