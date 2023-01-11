namespace PdfUploader.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = $"{Root}/{Version}"; // "api/v1"


        public static class Documents
        {
            public const string GetAll = $"{Base}/documents";
            public const string Get = Base + "/documents/{documentId}";
        }

        public static class FileUpload
        {
            public const string GetAll = $"{Base}/FileUpload";
            public const string Get = Base + "/FileUpload/{blobName}";
        }
        public static class Categories
        {
            public const string GetAll = $"{Base}/Categories";
            public const string Get = Base + "/Categories/{categoryId}";
        }

        public static class Identity
        {
            public const string Login = $"{Base}/identity/login";
            public const string Register = Base + "/identity/register";
        }
    }
}
