using PdfUploader.Models;

namespace PdfUploader.Services.Interfaces
{
    public interface ICategoriesService
    {
        public Task<IEnumerable<Category>> GetAll();
        public Task<Category> GetById(int id);
    }
}
