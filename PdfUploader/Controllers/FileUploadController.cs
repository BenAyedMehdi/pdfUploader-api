using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfUploader.Models;
using PdfUploader.Services;

namespace PdfUploader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IStorageService _blobStorage;

        public FileUploadController(IStorageService storageService)
        {
            _blobStorage = storageService;
        }
        public IActionResult Get()
        {
            return Ok("The server is working");
        }
        

        [HttpGet]
        [Route("download/{blobName}")]
        public async Task<IActionResult> Download(string blobName) //Returns 500
        {
            if (blobName == null) return BadRequest();
            await _blobStorage.DownloadBlob(blobName);
            return Ok();
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> ListBlobs() 
        {
            return Ok(await _blobStorage.ListBlobsAsync());
        }

        
        [HttpGet]
        [Route("{blobName}")]
        public async Task<IActionResult> GetBlobAsync(string blobName)
        {
            if (blobName == null) return BadRequest();
            var blob = await _blobStorage.GetBlobAsync(blobName);
            return File(blob.Content.ToArray(), blob.Details.ContentType);
        }
        

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadByPath([FromBody] UploadFileRequest request)
        {
            if (request.filePath == null) return BadRequest();
            await _blobStorage.UploadByFilePath(request.filePath, request.fileName); 
            return Ok();
        }

        [HttpPost]
        [Route("uploadfile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null) return BadRequest();
            await _blobStorage.Upload(file);
            return Ok(file);
        }

        [HttpDelete]
        [Route("{blobName}")]
        public async Task<IActionResult> Delete(string blobName)
        {
            if (blobName == null) return BadRequest();
            await _blobStorage.DeleteBlobAsync(blobName);
            return Ok();
        } 
    }
}
