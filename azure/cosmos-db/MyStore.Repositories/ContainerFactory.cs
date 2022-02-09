using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyStore.Infrastructure
{
    public sealed class ContainerFactory : IHostedService
    {
        private readonly ILogger<ContainerFactory> logger;

        private readonly DatabaseFactory databaseFactory;

        private Container products;

        private Container categories;

        public Container GetProductsContainer()
        {
            return products ?? throw new InvalidOperationException("Cosmos was not initialized.");
        }

        internal Container GetCategoriesContainer()
        {
            return categories ?? throw new InvalidOperationException("Cosmos was not initialized.");
        }

        public ContainerFactory(ILogger<ContainerFactory> logger, DatabaseFactory databaseFactory)
        {
            this.logger = logger;
            this.databaseFactory = databaseFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Database database = databaseFactory.GetDatabase();

            this.products = await database.CreateContainerIfNotExistsAsync("Products", "/partitionKey", cancellationToken: cancellationToken);
            logger.LogInformation("Container created: {0}", products.Id);

            this.categories = await database.CreateContainerIfNotExistsAsync("Categories", "/partitionKey", cancellationToken: cancellationToken);
            logger.LogInformation("Container created: {0}", categories.Id);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
