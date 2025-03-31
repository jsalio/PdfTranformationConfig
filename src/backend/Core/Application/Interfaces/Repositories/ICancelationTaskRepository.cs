using PdfConverter.Core.Application.Interfaces.Repositories;

namespace Backend.Core.Application.Interfaces.Repositories
{
    using Backend.Core.Domain.Entities;
    
    public interface ICancelationTaskRepository : IRepository<CancelationTask>
    {
        Task<IEnumerable<CancelationTask>> GetByWorkTaskIdAsync(int workTaskId);
    }
} 