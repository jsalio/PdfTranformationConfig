namespace Backend.Core.Domain.Entities
{
    public class CancelationTask
    {
        public int Id { get; set; }
        public int WorkTaskId { get; set; }
        public string Justificacion { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        
        // Navegación
        public WorkTask? WorkTask { get; set; }
    }
} 