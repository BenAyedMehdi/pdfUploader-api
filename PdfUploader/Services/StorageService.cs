using Azure.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using PdfUploader.Services.Interfaces;

namespace PdfUploader.Services
{
    public class StorageService : IStorageService
    {
        private readonly IConfiguration _config;
        private readonly BlobServiceClient _client;

        public StorageService(BlobServiceClient blobServiceClient , IConfiguration configuration)
        {
            _config = configuration;
            _client = blobServiceClient;
        }

        //All blobs
        public async Task<IEnumerable<string>> ListBlobsAsync()
        {
            var containerName = _config.GetConnectionString("ContainerName");
            var blobContainer = _client.GetBlobContainerClient(containerName);
            var items = new List<string>();
            await foreach (var blobItem in blobContainer.GetBlobsAsync())
            {
                items.Add(blobItem.Name);
            }
            return items;
        }

        //
        public async Task<BlobDownloadResult> GetBlobAsync(string blobName)
        {
            var containerName = _config.GetConnectionString("ContainerName");
            var blobContainer = _client.GetBlobContainerClient(containerName);
            var blobClient = blobContainer.GetBlobClient(blobName);
            var downloadInfo = await blobClient.DownloadContentAsync();
            return downloadInfo;
        }


        public  async Task DownloadBlob(string blobName)
        {
            var localFilePath = @"C:\";
            var containerName = _config.GetConnectionString("ContainerName");
            var blobContainer = _client.GetBlobContainerClient(containerName);
            var blobClient = blobContainer.GetBlobClient(blobName);

            try
            {
                    await blobClient.DownloadToAsync(localFilePath);
            }
            catch (DirectoryNotFoundException ex)
            {
                // Let the user know that the directory does not exist
                Console.WriteLine($"Directory not found: {ex.Message}");
            }
        }

        public async Task Upload(IFormFile formFile)
        {
            var contentType = formFile.ContentType;
            var containerName = _config.GetConnectionString("ContainerName");
            var blobContainer = _client.GetBlobContainerClient(containerName);
            var blobClient = blobContainer.GetBlobClient(formFile.FileName);

            using var stream = formFile.OpenReadStream();
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = contentType }  ) ;
        }

        public async Task UploadByFilePath(string filePath, string fileName)
        {
            var containerName = _config.GetConnectionString("ContainerName");
            var blobContainer = _client.GetBlobContainerClient(containerName);
            var blobClient = blobContainer.GetBlobClient(fileName);

            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(filePath, out contentType);
            await blobClient.UploadAsync(filePath, new BlobHttpHeaders { ContentType = contentType });
        }

        public async Task DeleteBlobAsync(string blobName)
        {
            var containerName = _config.GetConnectionString("ContainerName");
            var blobContainer = _client.GetBlobContainerClient(containerName);
            var blobClient = blobContainer.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();

        }
    }
}
