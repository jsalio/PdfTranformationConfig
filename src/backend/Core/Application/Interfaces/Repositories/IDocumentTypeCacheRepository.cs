using PdfConverter.Core.Application.Interfaces.Repositories;

namespace Backend.Core.Application.Interfaces.Repositories
{
    using Backend.Core.Domain.Entities;
    
    public interface IDocumentTypeCacheRepository : IRepository<DocumentTypeCache>
    {
        Task<DocumentTypeCache?> GetByDocumentTypeIdAsync(int documentTypeId);
        Task<DocumentTypeCache?> GetByDocumentTypeNameAsync(string documentTypeName);
    }
} 