using PdfConverter.Core.Application.Interfaces.Repositories;

namespace Backend.Core.Application.Interfaces.Repositories
{
    using Backend.Core.Domain.Entities;
    
    public interface IDocumentRetryLogRepository : IRepository<DocumentRetryLog>
    {
        Task<IEnumerable<DocumentRetryLog>> GetByLockIdAsync(int lockId);
        Task<IEnumerable<DocumentRetryLog>> GetByDocumentIdAsync(string documentId);
    }
} 