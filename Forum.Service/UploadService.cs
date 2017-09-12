using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Forum.Service
{
    public class UploadService : IUpload
    {
        public IConfiguration Configuration;

        public UploadService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public CloudBlobContainer GetBlobContainer()
        {
            var connectionString = Configuration["AzureStorageAccountConnectionString"];
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference("staticcontent");
        }
    }
}
