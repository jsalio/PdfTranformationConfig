using PdfConverter.Core.Application.Interfaces.Repositories;

namespace Backend.Core.Application.Interfaces.Repositories
{
    using Backend.Core.Domain.Entities;
    
    public interface IWorkflowCacheRepository : IRepository<WorkflowCache>
    {
        Task<WorkflowCache?> GetByWorkflowIdAsync(int workflowId);
        Task<WorkflowCache?> GetByWorkflowNameAsync(string workflowName);
    }
} 