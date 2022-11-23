using Microsoft.EntityFrameworkCore;
using PdfUploader.Data;
using PdfUploader.Models;
using PdfUploader.Models.Requests;
using PdfUploader.Services.Interfaces;

namespace PdfUploader.Services
{
    public class DocumentsService : IDocumentsService
    {
        private readonly DocumentsDbContext _context;

        public DocumentsService(DocumentsDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Document>> GetAll()
        {
            var list = await _context.Documents.ToListAsync();
            return list;
        }

        public async Task<Document> GetById(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if(document == null) return null;
            return document;    
        }

        public async Task<Document> Create(AddDocumentRequest request)
        {
            var doc = new Document
            {
                Title = request.Title,
                Description = request.Description,
                BlobName = request.BlobName
            };
            _context.Documents.Add(doc);    
            await _context.SaveChangesAsync();
            return doc;
        }


        public async Task<Document> Update(int id, Document request)
        {
            if (request.DocumentId != id) return null;
            var old = await GetById(id);
            if (old == null) return null;
            old.Title = request.Title; 
            old.Description= request.Description;
            old.BlobName = request.BlobName;
            _context.Entry(old).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return old;
        }
        public async Task Delete(int id)
        {
            var doc = await _context.Documents.FindAsync(id);
            if (doc == null) return;
            _context.Documents.Remove(doc);
            await _context.SaveChangesAsync();
        }
        public bool Exists(int id)
        {
            return _context.Documents.Any(e => e.DocumentId == id);
        }
    }
}
