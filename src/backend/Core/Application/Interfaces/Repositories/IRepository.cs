using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PdfConverter.Core.Application.Interfaces.Repositories
{
    /// <summary>
    /// Define operaciones genéricas para el acceso a datos de entidades.
    /// </summary>
    /// <typeparam name="T">El tipo de entidad con la que trabaja el repositorio.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Obtiene una entidad por su identificador.
        /// </summary>
        /// <param name="id">El identificador de la entidad a obtener.</param>
        /// <returns>La entidad encontrada o null si no existe.</returns>
        Task<T?> GetByIdAsync(int id);
        
        /// <summary>
        /// Obtiene todas las entidades.
        /// </summary>
        /// <returns>Una colección con todas las entidades.</returns>
        Task<IEnumerable<T>> GetAllAsync();
        
        /// <summary>
        /// Busca entidades que cumplan con el predicado especificado.
        /// </summary>
        /// <param name="predicate">El predicado que deben cumplir las entidades.</param>
        /// <returns>Una colección con las entidades que cumplen el predicado.</returns>
        Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate);
        
        /// <summary>
        /// Agrega una nueva entidad.
        /// </summary>
        /// <param name="entity">La entidad a agregar.</param>
        Task AddAsync(T entity);
        
        /// <summary>
        /// Actualiza una entidad existente.
        /// </summary>
        /// <param name="entity">La entidad a actualizar.</param>
        Task UpdateAsync(T entity);
        
        /// <summary>
        /// Elimina una entidad.
        /// </summary>
        /// <param name="entity">La entidad a eliminar.</param>
        Task DeleteAsync(T entity);
        
        /// <summary>
        /// Guarda los cambios realizados en el repositorio.
        /// </summary>
        Task SaveChangesAsync();
    }
} 