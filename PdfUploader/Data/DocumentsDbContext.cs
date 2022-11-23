using Microsoft.EntityFrameworkCore;
using PdfUploader.Models;

namespace PdfUploader.Data
{
    public class DocumentsDbContext : DbContext
    {
        public DocumentsDbContext(DbContextOptions<DocumentsDbContext> options) : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
    }
}
