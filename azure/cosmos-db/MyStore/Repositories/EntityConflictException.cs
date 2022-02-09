using System;

namespace MyStore.Repositories
{
    public sealed class EntityConflictException : Exception
    {
        public EntityConflictException(Exception innerException) : base("The entity with the specified id already exists.", innerException)
        {

        }
    }
}
