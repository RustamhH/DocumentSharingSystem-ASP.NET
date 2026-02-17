namespace DocumentSharingSystem.Models
{
    public class Document:BaseEntity
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadTime { get; set; } = DateTime.Now;
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public ICollection<Token> Tokens { get; set; } = new List<Token>();
    }
}
