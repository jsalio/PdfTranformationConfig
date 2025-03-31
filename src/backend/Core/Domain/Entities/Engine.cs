using PdfConverter.Core.Domain.Entities;

namespace Backend.Core.Domain.Entities
{
    public class Engine
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty; // JSON
        public bool AcceptOcr { get; set; }
        
        // Navegación
        public ICollection<Configuration>? Configurations { get; set; }
    }
} 