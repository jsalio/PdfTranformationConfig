namespace Backend.Core.Domain.Entities
{
    public class DocumentRetryLog
    {
        public int Id { get; set; }
        public int LockId { get; set; }
        public string DocumentId { get; set; } = string.Empty;
        public DateTime LastRetry { get; set; }
        public string ErrorReason { get; set; } = string.Empty;
        public int ValidForCount { get; set; }
        
        // Navegación
        public DocumentLock? DocumentLock { get; set; }
    }
} 