using PdfConverter.Core.Application.Interfaces.Repositories;

namespace Backend.Core.Application.Interfaces.Repositories
{
    using Backend.Core.Domain.Entities;
    
    public interface IEngineRepository : IRepository<Engine>
    {
        Task<Engine?> GetByNameAsync(string name);
        Task<IEnumerable<Engine>> GetEnginesWithOcrSupportAsync();
    }
} 