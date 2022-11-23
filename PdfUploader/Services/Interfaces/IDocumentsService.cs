using PdfUploader.Models;
using PdfUploader.Models.Requests;

namespace PdfUploader.Services.Interfaces
{
    public interface IDocumentsService
    {
        public Task<IEnumerable<Document>> GetAll();
        public Task<Document> GetById(int id);
        public Task<Document> Create(AddDocumentRequest request);
        public Task Delete(int id);
        public bool Exists(int id);
        public Task<Document> Update(int id, Document request);
    }
}
