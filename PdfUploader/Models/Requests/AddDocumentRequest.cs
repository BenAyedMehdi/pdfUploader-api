namespace PdfUploader.Models.Requests
{
    public class AddDocumentRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string BlobName { get; set; }
    }
}
