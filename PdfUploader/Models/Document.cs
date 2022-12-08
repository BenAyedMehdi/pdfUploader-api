namespace PdfUploader.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string BlobName { get; set; }
        public int DocumentPosition { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
        public int Views { get; set; } = 0;
        public int Dowloads { get; set; } = 0;  

    }
}
