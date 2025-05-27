using Azure.Storage.Blobs;
using EduSyncAPI.Interfaces;

namespace EduSyncAPI.Services
{
    public class BlobService : IBlobService
    {
        private readonly IConfiguration _config;

        public BlobService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string containerName)
        {
            var connectionString = _config["AzureBlob:ConnectionString"];
            var containerClient = new BlobContainerClient(connectionString, containerName);

            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(Guid.NewGuid() + Path.GetExtension(file.FileName));

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream);
            }

            return blobClient.Uri.ToString();
        }
    }

}
