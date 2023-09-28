

using Framework.comman.interfaces;
using Framework.core.IRerepositories.Base;
using Service.Comman.Enums;

namespace Service.Entities.entities
{
    public class Bill : IEntityIdentity<int>, ICreationTimeSignature, IDeletionSignature, IDateTimeSignature, IEntityUserSignature
    {
        public int Id { get; set; }
        public decimal amount { get; set; }
        public DateTime dateTime { get; set; }
        public StatusBill status { get; set; }
        public int patientId { get; set; }
        public Patient patient { get; set; } = null!;
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
