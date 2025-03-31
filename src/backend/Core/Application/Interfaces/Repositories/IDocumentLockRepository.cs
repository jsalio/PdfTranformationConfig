using PdfConverter.Core.Application.Interfaces.Repositories;

namespace Backend.Core.Application.Interfaces.Repositories
{
    using Backend.Core.Domain.Entities;
    
    public interface IDocumentLockRepository : IRepository<DocumentLock>
    {
        Task<DocumentLock?> GetByDocumentIdAsync(string documentId);
        Task<IEnumerable<DocumentLock>> GetExpiredLocksAsync(TimeSpan expirationTime);
    }
} 