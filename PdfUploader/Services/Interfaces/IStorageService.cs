using Azure.Storage.Blobs.Models;

namespace PdfUploader.Services.Interfaces
{
    public interface IStorageService
    {
        Task Upload(IFormFile formFile);
        Task<BlobDownloadResult> GetBlobAsync(string blobName);
        Task DownloadBlob(string blobName);
        Task<IEnumerable<string>> ListBlobsAsync();
        Task UploadByFilePath(string filePath, string fileName);
        Task DeleteBlobAsync(string blobName);
    }
}
