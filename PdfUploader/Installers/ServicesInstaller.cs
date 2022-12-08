using PdfUploader.Services.Interfaces;
using PdfUploader.Services;

namespace PdfUploader.Installers
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IStorageService, StorageService>();
            services.AddScoped<IDocumentsService, DocumentsService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
        }
    }
}
