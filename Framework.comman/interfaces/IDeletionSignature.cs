

namespace Framework.comman.interfaces
{
    public interface IDeletionSignature
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletionDate { get; set; }
        public long? DeletedByUserId { get; set; }
        public bool? MustDeletedPhysical { get; set; }

    }
}

/*
 
  IDeletionSignature:

  Purpose: This interface represents entities that have information about deletion.

  Explanation: It defines several properties related to deletion,
  such as IsDeleted (a flag indicating whether the entity is deleted), 
  DeletionDate (the date and time of deletion), DeletedByUserId (the user ID of the user who deleted the entity),
  and MustDeletedPhysical (a flag indicating whether the entity must be physically deleted).

  Use: Entities implementing this interface can track deletion-related information and allow for logical
  or physical deletion based on the requirements of the application.

 */
