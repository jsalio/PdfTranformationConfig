using System.Threading.Tasks;
using PdfConverter.Core.Domain.Entities;

namespace PdfConverter.Core.Application.Interfaces.Repositories
{
    /// <summary>
    /// Define operaciones específicas para el acceso a datos de configuraciones.
    /// </summary>
    public interface IConfigurationRepository : IRepository<Configuration>
    {
        /// <summary>
        /// Obtiene una configuración por su nombre.
        /// </summary>
        /// <param name="name">El nombre de la configuración a buscar.</param>
        /// <returns>La configuración encontrada o null si no existe.</returns>
        Task<Configuration?> GetByNameAsync(string name);

        /// <summary>
        /// Obtiene configuraciones por el ID del motor.
        /// </summary>
        /// <param name="engineId">El ID del motor.</param>
        /// <returns>Las configuraciones encontradas.</returns>
        Task<IEnumerable<Configuration>> GetByEngineIdAsync(int engineId);
    }
} 