namespace PdfUploader.Models.Requests
{
    public class AddDocumentRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string BlobName { get; set; }
        public int DocumentPosition { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
        public int Views { get; set; } = 0;
        public int Dowloads { get; set; } = 0;
        public int CategoryId { get; set; }
        public bool IsPublic { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
