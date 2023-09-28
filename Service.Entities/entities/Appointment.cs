

using Framework.comman.interfaces;
using Framework.core.IRerepositories.Base;
using Service.Comman.Enums;

namespace Service.Entities.entities
{
    public class Appointment : IEntityIdentity<int>,ICreationTimeSignature,IDeletionSignature,IDateTimeSignature,IEntityUserSignature
    {
        public int Id { get; set; }
        public DateTime dateTime { get; set; }
        public StatusAppointment status { get; set; }
        public int patientId { get; set; }
        public Patient patient { get; set; } = null!;
        public int doctorId { get; set; }
        public Doctor doctor { get; set; } = null!;
        public ICollection<Prescription> prescriptions { get; set; } = new List<Prescription>();
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
