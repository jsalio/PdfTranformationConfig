using PdfConverter.Core.Application.Interfaces.Repositories;

namespace Backend.Core.Application.Interfaces.Repositories
{
    using Backend.Core.Domain.Entities;
    using Backend.Core.Domain.Enums;
    
    public interface IWorkTaskRepository : IRepository<WorkTask>
    {
        Task<IEnumerable<WorkTask>> GetByStatusAsync(WorkTaskStatus status);
        Task<IEnumerable<WorkTask>> GetByConfigurationIdAsync(int configurationId);
        Task<WorkTask?> GetWithDetailsAsync(int id);
    }
} 