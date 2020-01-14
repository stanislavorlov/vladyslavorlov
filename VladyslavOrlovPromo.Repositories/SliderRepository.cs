using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Linq;
using System.Threading.Tasks;
using VladyslavOrlovPromo.Core.Configs;

namespace VladyslavOrlovPromo.Repositories
{
    public class SliderRepository
    {
        private readonly SliderStorageConfiguration _sliderStorageConfig;

        public SliderRepository(IOptions<SliderStorageConfiguration> sliderStorageConfig)
        {
            _sliderStorageConfig = sliderStorageConfig.Value;
        }

        public async Task<string> Fetch()
        {
            var storageConnectionString = _sliderStorageConfig.ConnectionString;
            CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);

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