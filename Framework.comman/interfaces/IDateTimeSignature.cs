

namespace Framework.comman.interfaces
{
    public interface IDateTimeSignature : ICreationTimeSignature
    {
        DateTime? FirstModificationDate { get; set; }
        DateTime? LastModificationDate { get; set; }

    }
}

/*
   Purpose: This interface extends ICreationTimeSignature and represents entities that have additional date
   and time signatures.

   Explanation: It adds two properties, FirstModificationDate and LastModificationDate, both of type DateTime?, 
   representing the dates and times of the first and last modifications made to the entity, respectively.

   Use: By implementing this interface, entities can track the first and last modification dates in addition to
   the creation date.
   
 */