using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PdfUploader.Models;

namespace PdfUploader.Data
{
    public class DocumentsDbContext :  IdentityDbContext
    {
        public DocumentsDbContext(DbContextOptions<DocumentsDbContext> options) : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
