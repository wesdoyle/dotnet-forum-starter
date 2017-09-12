using Microsoft.WindowsAzure.Storage.Blob;

namespace Forum.Service
{
    public interface IUpload
    {
        CloudBlobContainer GetBlobContainer(string connectionString);
    }
}
