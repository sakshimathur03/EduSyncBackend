using Azure.Storage.Blobs;
using EduSyncAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EduSyncAPI.Services
{
    public class BlobService : IBlobService
    {
        private readonly string _blobConnectionString;

        public BlobService(IConfiguration config)
        {
            _blobConnectionString = config["AzureBlob:ConnectionString"];

            if (string.IsNullOrWhiteSpace(_blobConnectionString))
                throw new ArgumentNullException("AzureBlob:ConnectionString is not configured properly in appsettings.json");
        }

        public async Task<string> UploadFileAsync(IFormFile file, string containerName)
        {
            var containerClient = new BlobContainerClient(_blobConnectionString, containerName);
            await containerClient.CreateIfNotExistsAsync();

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            return blobClient.Uri.ToString();
        }
    }
}
