using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DocumentsDbContext>();


            services.AddAzureClients(b => {
                b.AddBlobServiceClient(configuration.GetConnectionString("BlobStorage"));
            });
        }
    }
}
