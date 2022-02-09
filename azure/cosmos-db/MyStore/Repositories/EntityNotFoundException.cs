using System;

namespace MyStore.Repositories
{
    public class EntityNotFoundException: Exception
    {
        public EntityNotFoundException(Exception innerException) : base("The entity with the specified id does not exist.", innerException)
        {

        }
    }
}
