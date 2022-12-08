using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfUploader.Contracts.V1;
using PdfUploader.Models.Requests;
using PdfUploader.Services.Interfaces;

namespace PdfUploader.Controllers.V1
{
    [Route(ApiRoutes.FileUpload.GetAll)]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IStorageService _blobStorage;

        public FileUploadController(IStorageService storageService)
        {
            _blobStorage = storageService;
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
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (file == null) return BadRequest();
            var res = await _blobStorage.Upload(file);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.FileUpload.Get.Replace("{blobName}", res.ToString());
            return Created(locationUrl, res);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string blobName)
        {
            if (blobName == null) return BadRequest();
            await _blobStorage.DeleteBlobAsync(blobName);
            return Ok();
        }

        /*
        [HttpPost]
        [Route("path")]
        public async Task<IActionResult> UploadByPath([FromBody] UploadFileRequest request)
        {
            if (request.filePath == null) return BadRequest();
            await _blobStorage.UploadByFilePath(request.filePath, request.fileName);
            return Ok();
        }

        [HttpGet]
        [Route("download/{blobName}")]
        public async Task<IActionResult> Download(string blobName) //Returns 500
        {
            if (blobName == null) return BadRequest();
            await _blobStorage.DownloadBlob(blobName);
            return Ok();
        }

        protected IActionResult Get()
        {
            return Ok("The server is working");
        }
        */
    }
}
