namespace Backend.Core.Domain.Enums
{
    /// <summary>
    /// Enumera los posibles estados de una tarea de trabajo.
    /// </summary>
    public enum WorkTaskStatus
    {
        /// <summary>
        /// La tarea está pendiente de procesamiento.
        /// </summary>
        Pending = 0,
        
        /// <summary>
        /// La tarea está siendo procesada actualmente.
        /// </summary>
        Working = 1,
        
        /// <summary>
        /// La tarea está pausada temporalmente.
        /// </summary>
        Paused = 2,
        
        /// <summary>
        /// La tarea ha sido cancelada.
        /// </summary>
        Canceled = 3
    }
} 