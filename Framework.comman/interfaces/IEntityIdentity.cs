namespace Framework.core.IRerepositories.Base
{
    public interface IEntityIdentity<TPrimaryKey>
    {
        TPrimaryKey Id { get; }
    }
}

/*
    IEntityIdentity<TPrimaryKey>:

    Purpose: This interface represents entities that have an identity or primary key.
    Explanation: It defines a single property Id of generic type TPrimaryKey that represents the identity or primary key of the entity.
    Use: By implementing this interface, entities can provide a consistent way to access and manipulate their identity properties.
 
 */