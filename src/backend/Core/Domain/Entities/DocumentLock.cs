namespace Backend.Core.Domain.Entities
{
    public class DocumentLock
    {
        public int Id { get; set; }
        public string DocumentId { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime LastWork { get; set; }
        
        // Navegación
        public ICollection<DocumentRetryLog>? DocumentRetryLogs { get; set; }
    }
} 