using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using PdfUploader.Data;
using PdfUploader.Installers;
using PdfUploader.Services;
using PdfUploader.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var installers = typeof(Program).Assembly.ExportedTypes
    .Where(type => typeof(IInstaller).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
    .Select(Activator.CreateInstance)
    .Cast<IInstaller>()
    .ToList();
installers.ForEach(installer => installer.InstallServices(builder.Services, builder.Configuration));


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PDF upload");
    c.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("MyAllowedOrigins");
app.MapControllers();

app.Run();
