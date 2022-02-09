using Microsoft.Azure.Cosmos;

namespace MyStore.Repositories
{
    internal static class Globals
    {
        public static readonly ItemRequestOptions DisableContentResponseOnWrite = new ItemRequestOptions()
        {
            EnableContentResponseOnWrite = false
        };
    }
}
