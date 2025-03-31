using PdfConverter.Core.Application.Interfaces.Repositories;

namespace Backend.Core.Application.Interfaces.Repositories
{
    using Backend.Core.Domain.Entities;
    
    public interface IDocumentConfigurationRepository : IRepository<DocumentConfiguration>
    {
        Task<IEnumerable<DocumentConfiguration>> GetByConfigurationIdAsync(int configurationId);
        Task<DocumentConfiguration?> GetByConfigAndDocTypeAsync(int configurationId, int documentTypeId);
    }
} 