using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyStore.Infrastructure
{
    public sealed class DatabaseFactory : IHostedService, IDisposable
    {
        private readonly ILogger<DatabaseFactory> logger;

        private readonly IProductsSettings settings;

        private readonly CosmosClient cosmosClient;

        private Database database;

        public DatabaseFactory(ILogger<DatabaseFactory> logger, IProductsSettings settings)
        {
            this.logger = logger;

            this.settings = settings;

            this.cosmosClient = new CosmosClient(settings.EndpointUri, settings.PrimaryKey, new CosmosClientOptions() { ApplicationName = settings.ApplicationName });
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            this.database = await cosmosClient.CreateDatabaseIfNotExistsAsync(settings.DatabaseId, cancellationToken: cancellationToken);

            logger.LogInformation("Database created: {0}", database.Id);
        }

        public Database GetDatabase()
        {
            return database ?? throw new InvalidOperationException("Cosmos not initialized");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            cosmosClient.Dispose();
        }
    }
}
