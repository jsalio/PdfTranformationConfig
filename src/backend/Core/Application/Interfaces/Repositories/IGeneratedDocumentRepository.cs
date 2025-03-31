using PdfConverter.Core.Application.Interfaces.Repositories;

namespace Backend.Core.Application.Interfaces.Repositories
{
    using Backend.Core.Domain.Entities;
    
    public interface IGeneratedDocumentRepository : IRepository<GeneratedDocument>
    {
        Task<IEnumerable<GeneratedDocument>> GetByWorkTaskIdAsync(int workTaskId);
        Task<GeneratedDocument?> GetByDocumentIdAsync(string documentId);
    }
} 