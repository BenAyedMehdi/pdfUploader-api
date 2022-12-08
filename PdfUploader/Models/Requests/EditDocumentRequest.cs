namespace PdfUploader.Models.Requests
{
    public class EditDocumentRequest
    {
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BlobName { get; set; }
        public int DocumentPosition { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
        public int Views { get; set; } 
        public int Dowloads { get; set; }
        public int CategoryId { get; set; }
    }
}
