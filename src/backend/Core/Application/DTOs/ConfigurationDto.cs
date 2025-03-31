namespace Backend.Core.Application.DTOs
{
    public class ConfigurationDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int WorkflowId { get; set; }
        public int EngineId { get; set; }
        public int RetryLimit { get; set; }
    }
} 