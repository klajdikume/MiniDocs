using Azure.Storage.Blobs;

namespace MiniDocs.API.Services
{
    public class BlobStorageService
    {
        private readonly BlobContainerClient _containerClient;

        public BlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration["AzureBlobStorage:ConnectionString"];
            var containerName = configuration["AzureBlobStorage:ContainerName"];

            var blobServiceClient = new BlobServiceClient(connectionString);
            _containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        }

        public async Task UploadCompressedVersionAsync(string blobName, string compressedData)
        {
            await _containerClient.CreateIfNotExistsAsync();
            var blobClient = _containerClient.GetBlobClient(blobName);

            var bytes = Convert.FromBase64String(compressedData);
            using var stram = new MemoryStream(bytes);

            await blobClient.UploadAsync(stram, overwrite: true);
        }
    }
}
