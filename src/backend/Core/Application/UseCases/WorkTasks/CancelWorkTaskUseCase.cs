namespace Backend.Core.Application.UseCases.WorkTasks
{
    using System;
    using System.Threading.Tasks;
    using Backend.Core.Application.Interfaces.Repositories;
    using Backend.Core.Application.Interfaces.UseCases;
    using Backend.Core.Domain.Entities;
    using Backend.Core.Domain.Enums;
    
    public class CancelWorkTaskUseCase : IUseCase<(int id, string justification), bool>
    {
        private readonly IWorkTaskRepository _workTaskRepository;
        private readonly ICancelationTaskRepository _cancelationTaskRepository;
        
        public CancelWorkTaskUseCase(
            IWorkTaskRepository workTaskRepository,
            ICancelationTaskRepository cancelationTaskRepository)
        {
            _workTaskRepository = workTaskRepository;
            _cancelationTaskRepository = cancelationTaskRepository;
        }
        
        public async Task<bool> ExecuteAsync((int id, string justification) request)
        {
            var (id, justification) = request;
            
            var workTask = await _workTaskRepository.GetByIdAsync(id);
            if (workTask == null)
                return false;
                
            // Solo se pueden cancelar tareas pendientes o en proceso
            if (workTask.Status != WorkTaskStatus.Pending && workTask.Status != WorkTaskStatus.Working)
                return false;
                
            workTask.Status = WorkTaskStatus.Canceled;
            
            // Registrar la cancelación
            var cancelationTask = new CancelationTask
            {
                WorkTaskId = id,
                Justificacion = justification,
                Created = DateTime.UtcNow
            };
            
            await _cancelationTaskRepository.AddAsync(cancelationTask);
            await _workTaskRepository.UpdateAsync(workTask);
            await _workTaskRepository.SaveChangesAsync();
            
            return true;
        }
    }
} 