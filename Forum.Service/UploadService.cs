using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Forum.Service
{
    public class UploadService
    {
        public CloudBlobContainer GetBlobContainer()
        {
            var connectionString = _configuration["AzureStorageAccountConnectionString"];
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference("staticcontent");

        }
    }
}
