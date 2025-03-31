using System.Threading.Tasks;
using PdfConverter.Core.Domain.Entities;

namespace PdfConverter.Core.Application.Interfaces.Repositories
{
    public interface IConfigurationRepository : IRepository<Configuration>
    {
        Task<Configuration> GetByNameAsync(string name);
    }
} 