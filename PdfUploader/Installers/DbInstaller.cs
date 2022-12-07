using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using PdfUploader.Data;

namespace PdfUploader.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DocumentsDbContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("DefaultConnetion"));
            });


            services.AddAzureClients(b => {
                b.AddBlobServiceClient(configuration.GetConnectionString("BlobStorage"));
            });
        }
    }
}
