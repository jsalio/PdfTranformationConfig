namespace Backend.Core.Domain.Entities
{
    using Backend.Core.Domain.Enums;
    
    public class WorkTaskDetail
    {
        public int Id { get; set; }
        public int WorkTaskId { get; set; }
        public string DocumentId { get; set; } = string.Empty;
        public WorkTaskDetailStatus Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastDateWork { get; set; }
        
        // Navegación
        public WorkTask? WorkTask { get; set; }
    }
} 