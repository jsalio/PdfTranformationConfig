using PdfConverter.Core.Domain.Entities;

namespace Backend.Core.Domain.Entities
{
    public class DocumentConfiguration
    {
        public int Id { get; set; }
        public int ConfigurationId { get; set; }
        public int DocumentTypeId { get; set; }
        public bool ConvertToPDF { get; set; }
        public bool ApplyOcr { get; set; }
        
        // Navegación
        public Configuration? Configuration { get; set; }
    }
} 