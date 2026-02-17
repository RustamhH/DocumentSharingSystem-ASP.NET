namespace DocumentSharingSystem.Models
{
    public class Token:BaseEntity
    {
        public DateTime ExpiryTime { get; set; }
        public string TokenHash { get; set; }
        public bool IsUsed { get; set; }
        public int DownloadCount { get; set; }
        public bool IsRevoked { get; set; }

        public Document Document { get; set; }
        public string DocumentId { get; set; }
    }
}
