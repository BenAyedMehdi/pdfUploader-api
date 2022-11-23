using Microsoft.AspNetCore.Mvc;
using PdfUploader.Models;
using PdfUploader.Models.Requests;
using PdfUploader.Services;
using PdfUploader.Services.Interfaces;

namespace PdfUploader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentsService _documentsService;

        public DocumentsController(IDocumentsService documentsService)
        {
            _documentsService = documentsService;
        }

        // GET: api/<DocumentsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> Get()
        {
            var list = await _documentsService.GetAll();
            return Ok(list);
        }

        // GET api/<DocumentsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> Get(int id)
        {
            if (id == 0) return BadRequest();
            var document = await _documentsService.GetById(id);
            if(document == null) return NotFound();
            return document; 
        }

        // POST api/<DocumentsController>
        [HttpPost]
        public async Task<ActionResult<Document>> Post(AddDocumentRequest request)
        {
            if(request == null) return BadRequest();
            var res = await _documentsService.Create(request);
            return Ok(res);
        }

        // PUT api/<DocumentsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Document>> Put(int id, Document request)
        {
            if(id == 0) return BadRequest();
            if (!_documentsService.Exists(id)) return NotFound();
            var newDoc = await _documentsService.Update(id, request);
            return Ok(newDoc);
        }

        // DELETE api/<DocumentsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest();
            if (!_documentsService.Exists(id)) return NotFound();
            await _documentsService.Delete(id);
            return Ok();
        }
    }
}
