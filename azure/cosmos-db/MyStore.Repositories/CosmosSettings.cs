using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Infrastructure
{
    public class CosmosSettings : IProductsSettings
    {
        public string EndpointUri { get; set; }

        public string PrimaryKey { get; set; }

        public string DatabaseId { get; set; }

        public string ApplicationName { get; set; }
    }

    public interface IProductsSettings
    {
        string EndpointUri { get; }

        string PrimaryKey { get; }

        public string DatabaseId { get; }

        public string ApplicationName { get; }
    }
}
