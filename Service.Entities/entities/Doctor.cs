

using Framework.comman.interfaces;
using Framework.core.IRerepositories.Base;
using Service.Comman.Enums;

namespace Service.Entities.entities
{
    public class Doctor : IEntityIdentity<int>, ICreationTimeSignature, IDeletionSignature, IDateTimeSignature, IEntityUserSignature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Specialty? specialty { get; set; }
        public string license { get; set; }
        public string contact { get; set; }
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
