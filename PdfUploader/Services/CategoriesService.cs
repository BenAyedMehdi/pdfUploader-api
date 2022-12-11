using Azure.Core;
using Microsoft.EntityFrameworkCore;
using PdfUploader.Data;
using PdfUploader.Migrations;
using PdfUploader.Models;
using PdfUploader.Models.Requests;
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

        public async Task<Category> Create(AddCategoryRequest request)
        {

            var category = new Category
            {
                CategoryName = request.CategoryName
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
