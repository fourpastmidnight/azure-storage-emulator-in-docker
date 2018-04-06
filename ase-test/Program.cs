using System;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace ase_test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<AzureStorageSettings>(hostContext.Configuration.GetSection("Data:Azure"));
                    services.AddScoped<IHostedService, AseTestService>();
                });

            await builder.RunConsoleAsync();
        }
    }
}
