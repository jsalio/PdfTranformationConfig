using System;
using Backend.Core.Domain.Entities;

namespace PdfConverter.Core.Domain.Entities
{
    /// <summary>
    /// Representa una configuración para la conversión de documentos a PDF.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Obtiene el identificador único de la configuración.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtiene el nombre de la configuración.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene la descripción detallada de la configuración.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene un valor que indica si la configuración está activa.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Obtiene la fecha y hora de creación de la configuración.
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Obtiene la fecha y hora de la última actualización de la configuración.
        /// </summary>
        public DateTime? UpdatedAt { get; private set; }

        /// <summary>
        /// Obtiene el identificador del flujo de trabajo asociado a la configuración.
        /// </summary>
        public int WorkflowId { get; set; }

        /// <summary>
        /// Obtiene el identificador del motor asociado a la configuración.
        /// </summary>
        public int EngineId { get; set; }

        /// <summary>
        /// Obtiene el límite de reintentos para la configuración.
        /// </summary>
        public int RetryLimit { get; set; }

        // Navegación
        public Engine? Engine { get; set; }
        public ICollection<DocumentConfiguration>? DocumentConfigurations { get; set; }
        public ICollection<WorkTask>? WorkTasks { get; set; }

        // Constructor privado para EF Core
        public Configuration() { }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Configuration"/> con los valores especificados.
        /// </summary>
        /// <param name="name">El nombre de la configuración.</param>
        /// <param name="description">La descripción de la configuración.</param>
        /// <param name="isActive">Indica si la configuración está activa.</param>
        /// <exception cref="ArgumentException">Se lanza cuando el nombre está vacío o es nulo.</exception>
        public Configuration(string name, string description, bool isActive)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));

            Name = name;
            Description = description;
            IsActive = isActive;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Actualiza los datos de la configuración.
        /// </summary>
        /// <param name="name">El nuevo nombre de la configuración.</param>
        /// <param name="description">La nueva descripción de la configuración.</param>
        /// <param name="isActive">El nuevo estado de activación de la configuración.</param>
        /// <exception cref="ArgumentException">Se lanza cuando el nombre está vacío o es nulo.</exception>
        public void Update(string name, string description, bool isActive)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));

            Name = name;
            Description = description;
            IsActive = isActive;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Activa la configuración.
        /// </summary>
        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Desactiva la configuración.
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
} 