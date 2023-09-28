

namespace Framework.comman.interfaces
{
    public interface IEntityUserSignature : IEntityCreatedUserSignature
    {
        long? FirstModifiedByUserId { get; set; }
        long? LastModifiedByUserId { get; set; }

    }
}

/*
   Purpose: This interface extends IEntityCreatedUserSignature and represents entities that
   have information about user signatures.

   Explanation: It adds two properties FirstModifiedByUserId and LastModifiedByUserId, both of type long?,
   which represent the user IDs of the first and last users who modified the entity, respectively.

   Use: By implementing this interface, entities can track information about the users who made modifications to them.

 */