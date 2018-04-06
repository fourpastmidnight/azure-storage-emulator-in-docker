using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace ase_test
{
    public class AseTestService : IHostedService
    {
        public AseTestService(IOptions<AzureStorageSettings> settings) => AzureStorageSettings = settings; 

        public IOptions<AzureStorageSettings> AzureStorageSettings {get;}

        public async Task TestAzureStorageEmulator()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AzureStorageSettings.Value.StorageConnectionString);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("myqueue");
            await queue.CreateIfNotExistsAsync();

            CloudQueueMessage message = new CloudQueueMessage("Hello, World!");
            await queue.AddMessageAsync(message);

            CloudQueueMessage peekedMessage = await queue.PeekMessageAsync();

            Console.WriteLine(peekedMessage.AsString);

            var existingMessage = await queue.GetMessageAsync();
            existingMessage.SetMessageContent("Updated contents.");
            await queue.UpdateMessageAsync(existingMessage, TimeSpan.FromSeconds(60.0), MessageUpdateFields.Content | MessageUpdateFields.Visibility);

            var retrievedMessage = await queue.GetMessageAsync();
            Console.WriteLine($"Retrieved message: {retrievedMessage.AsString}");
            await queue.DeleteMessageAsync(retrievedMessage);
        }

        public async Task StartAsync(CancellationToken token)
        {
            await TestAzureStorageEmulator();
        }

        public Task StopAsync(CancellationToken token)
        {
            return Task.CompletedTask;
        }
    }
}
