using Microsoft.EntityFrameworkCore;
using PdfUploader.Data;
using PdfUploader.Models;
using PdfUploader.Services.Interfaces;

namespace PdfUploader.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly DocumentsDbContext _context;

        public CategoriesService(DocumentsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var list = await _context.Categories
                .ToListAsync();
            return list;
        }

        public async Task<Category> GetById(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == id);
            if (category == null) return null;
            return category;
        }
    }
}
