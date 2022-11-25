using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfUploader.Models.Requests;
using PdfUploader.Services.Interfaces;

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
        protected IActionResult Get()
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
        [Route("path")]
        public async Task<IActionResult> UploadByPath([FromBody] UploadFileRequest request)
        {
            if (request.filePath == null) return BadRequest();
            await _blobStorage.UploadByFilePath(request.filePath, request.fileName); 
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null) return BadRequest();
            await _blobStorage.Upload(file);
            return Ok(file); //works
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string blobName)
        {
            if (blobName == null) return BadRequest();
            await _blobStorage.DeleteBlobAsync(blobName);
            return Ok();
        } 
    }
}
