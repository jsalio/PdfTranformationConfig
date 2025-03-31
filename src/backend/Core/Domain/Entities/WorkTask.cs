using PdfConverter.Core.Domain.Entities;

namespace Backend.Core.Domain.Entities
{
    using Backend.Core.Domain.Enums;
    
    public class WorkTask
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public int ConfigurationId { get; set; }
        public WorkTaskStatus Status { get; set; }
        
        // Navegación
        public Configuration? Configuration { get; set; }
        public ICollection<WorkTaskDetail>? WorkTaskDetails { get; set; }
        public ICollection<CancelationTask>? CancelationTasks { get; set; }
        public ICollection<GeneratedDocument>? GeneratedDocuments { get; set; }
    }
} 