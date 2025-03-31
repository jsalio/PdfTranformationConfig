using PdfConverter.Core.Application.Interfaces.Repositories;

namespace Backend.Core.Application.Interfaces.Repositories
{
    using Backend.Core.Domain.Entities;
    using Backend.Core.Domain.Enums;
    
    public interface IWorkTaskDetailRepository : IRepository<WorkTaskDetail>
    {
        Task<IEnumerable<WorkTaskDetail>> GetByWorkTaskIdAsync(int workTaskId);
        Task<IEnumerable<WorkTaskDetail>> GetByStatusAsync(WorkTaskDetailStatus status);
        Task<WorkTaskDetail?> GetByDocumentIdAsync(string documentId);
    }
} 