using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Linq;
using System.Threading.Tasks;
using VladyslavOrlovPromo.Core.Configs;
using VladyslavOrlovPromo.Repositories.Interfaces;

namespace VladyslavOrlovPromo.Repositories
{
    public class SliderRepository : ISliderRepository
    {
        private readonly SliderStorageConfiguration _sliderStorageConfiguration;

        public SliderRepository(IOptions<SliderStorageConfiguration> sliderStorageConfig)
        {
            _sliderStorageConfiguration = sliderStorageConfig.Value;
        }

        public async Task<string> Fetch()
        {
            var client = new SecretClient(new Uri(_sliderStorageConfiguration.VaultUri), new DefaultAzureCredential());
            KeyVaultSecret secret = await client.GetSecretAsync(_sliderStorageConfiguration.SecretName);

            CloudStorageAccount account = CloudStorageAccount.Parse(secret.Value);

            CloudBlobClient serviceClient = account.CreateCloudBlobClient();

            var container = serviceClient.GetContainerReference("slidercontainer");
            container.CreateIfNotExistsAsync().Wait();

            //CloudBlockBlob blob = container.GetBlockBlobReference("helloworld.txt");
            //blob.UploadTextAsync("Hello, world!").Wait();


            BlobContinuationToken continuationToken = null;
            var blobResultSegment = await container.ListBlobsSegmentedAsync(continuationToken);
            var blobs = blobResultSegment.Results.Cast<CloudBlockBlob>().ToList();


            //return await blob.DownloadTextAsync();
            return string.Empty;
        }
    }
}