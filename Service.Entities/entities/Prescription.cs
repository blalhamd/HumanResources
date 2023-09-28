

using Framework.comman.interfaces;
using Framework.core.IRerepositories.Base;

namespace Service.Entities.entities
{
    public class Prescription : IEntityIdentity<int>,ICreationTimeSignature,IDeletionSignature,IDateTimeSignature, IEntityUserSignature
    {
        public int Id { get; set; }
        public string medication { get; set; }
        public string? dosage { get; set; }
        public int frequency { get; set; }   // The frequency of taking the medication prescribed
        public string duration { get; set; } //The duration of taking the medication prescribed
        public int AppointmentId { get; set; }
        public Appointment appointment { get; set; } = null!;
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
