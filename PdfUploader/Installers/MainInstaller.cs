using System.Reflection;

namespace PdfUploader.Installers
{
    public class MainInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddCors(options =>
            {
                options.AddPolicy("MyAllowedOrigins",
                    policy =>
                    {
                        policy.WithOrigins("*")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
