

namespace Framework.comman.interfaces
{
    public interface IEntityCreatedUserSignature
    {
        long? CreatedByUserId { get; set; }
    }
}

/*
   Purpose: This interface represents entities that have information about the user who created them.
   Explanation: It defines a single property CreatedByUserId of type long? that represents the user ID of the user who created the entity.
   Use: Entities implementing this interface can track information about the user who created them.

 */
