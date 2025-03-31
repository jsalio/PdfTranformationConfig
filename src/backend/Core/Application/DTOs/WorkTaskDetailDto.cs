namespace Backend.Core.Application.DTOs
{
    using Backend.Core.Domain.Enums;
    using System;
    
    public class WorkTaskDetailDto
    {
        public int Id { get; set; }
        public int WorkTaskId { get; set; }
        public string DocumentId { get; set; } = string.Empty;
        public WorkTaskDetailStatus Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastDateWork { get; set; }
    }
} 