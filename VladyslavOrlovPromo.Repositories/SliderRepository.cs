using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace VladyslavOrlovPromo.Repositories
{
    public class SliderRepository
    {
        private string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=voslider;AccountKey=4KNcVtxgqi82CceUGWMM+PG2pM2iTabaz3FfpiQnHyw6L4Sn+603QbVfR5QEbtYsBpyRJCQiCiNrG/RqzYjOPw==;EndpointSuffix=core.windows.net";

        public SliderRepository()
        {

        }

        public async Task<string> Fetch()
        {
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