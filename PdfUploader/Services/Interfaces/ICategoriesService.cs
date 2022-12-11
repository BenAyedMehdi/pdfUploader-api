using PdfUploader.Models;
using PdfUploader.Models.Requests;

namespace PdfUploader.Services.Interfaces
{
    public interface ICategoriesService
    {
        public Task<IEnumerable<Category>> GetAll();
        public Task<Category> GetById(int id);
        public Task<Category> Create(AddCategoryRequest request);
    }
}
