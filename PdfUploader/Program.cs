using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using PdfUploader.Data;
using PdfUploader.Services;
using PdfUploader.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowedOrigins",
        policy =>
        {
            policy.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddDbContext<DocumentsDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnetion"));
});

builder.Services.AddAzureClients(b =>
    b.AddBlobServiceClient(builder.Configuration.GetConnectionString("BlobStorage")));
builder.Services.AddSingleton<IStorageService, StorageService>();
builder.Services.AddScoped<IDocumentsService, DocumentsService>();


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
