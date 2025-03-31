namespace Backend.Core.Application.DTOs
{
    using Backend.Core.Domain.Enums;
    using System;
    
    public class WorkTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public int ConfigurationId { get; set; }
        public WorkTaskStatus Status { get; set; }
    }
}