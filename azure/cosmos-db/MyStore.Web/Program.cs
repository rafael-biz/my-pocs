using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyStore.Caching;
using MyStore.Repositories;
using MyStore.Services;

namespace MyStore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((builder, services) =>
                {
                    IConfiguration configuration = builder.Configuration;

                    services.AddRepositories(configuration.GetSection("Cosmos"));
                    services.AddServices();
                    services.AddCaching(configuration.GetSection("Redis"));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
