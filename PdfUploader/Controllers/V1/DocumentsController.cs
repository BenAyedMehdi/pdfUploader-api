﻿using Microsoft.AspNetCore.Mvc;
using PdfUploader.Contracts.V1;
using PdfUploader.Models;
using PdfUploader.Models.Requests;
using PdfUploader.Services;
using PdfUploader.Services.Interfaces;

namespace PdfUploader.Controllers.V1
{
    [Route(ApiRoutes.Documents.GetAll)]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentsService _documentsService;

        public DocumentsController(IDocumentsService documentsService)
        {
            _documentsService = documentsService;
        }

        /// <summary>
        /// Get a list of all documents
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> Get()
        {
            var list = await _documentsService.GetAll();
            return Ok(list);
        }

        /// <summary>
        /// Get a single document by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> Get(int id)
        {
            if (id == 0) return BadRequest();
            var document = await _documentsService.GetById(id);
            if (document == null) return NotFound();
            return document;
        }

        /// <summary>
        /// Create a new document
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Document>> Post(AddDocumentRequest request)
        {
            if (request == null) return BadRequest();
            var res = await _documentsService.Create(request);
            
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Documents.Get.Replace("{documentId}", res.DocumentId.ToString());
            return Created(locationUrl, res);
        }

        /// <summary>
        /// Update an existing document
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Document>> Put(int id, EditDocumentRequest request)
        {
            if (id == 0) return BadRequest();
            if (!_documentsService.Exists(id)) return NotFound();
            var newDoc = await _documentsService.Update(id, request);
            return Ok(newDoc);
        }

        /// <summary>
        /// Delete a document by ID (doesn't delete blob file)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
