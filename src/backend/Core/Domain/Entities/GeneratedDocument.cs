namespace Backend.Core.Domain.Entities
{
    public class GeneratedDocument
    {
        public int Id { get; set; }
        public int WorkTaskId { get; set; }
        public string DocumentId { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string Directory { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        
        // Navegación
        public WorkTask? WorkTask { get; set; }
    }
} 