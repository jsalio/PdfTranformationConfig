using PdfConverter.Core.Application.Interfaces.Repositories;

namespace Backend.Core.Application.Interfaces.Repositories
{
    using Backend.Core.Domain.Entities;
    
    public interface IProfileRepository : IRepository<Profile>
    {
        Task<Profile?> GetByNameAsync(string name);
    }
} 