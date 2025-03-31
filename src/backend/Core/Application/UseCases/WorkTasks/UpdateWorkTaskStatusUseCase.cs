namespace Backend.Core.Application.UseCases.WorkTasks
{
    using System.Threading.Tasks;
    using Backend.Core.Application.Interfaces.Repositories;
    using Backend.Core.Application.Interfaces.UseCases;
    using Backend.Core.Domain.Enums;
    
    public class UpdateWorkTaskStatusUseCase : IUseCase<(int id, WorkTaskStatus status), bool>
    {
        private readonly IWorkTaskRepository _workTaskRepository;
        
        public UpdateWorkTaskStatusUseCase(IWorkTaskRepository workTaskRepository)
        {
            _workTaskRepository = workTaskRepository;
        }
        
        public async Task<bool> ExecuteAsync((int id, WorkTaskStatus status) request)
        {
            var (id, status) = request;
            
            var workTask = await _workTaskRepository.GetByIdAsync(id);
            if (workTask == null)
                return false;
                
            workTask.Status = status;
            
            await _workTaskRepository.UpdateAsync(workTask);
            await _workTaskRepository.SaveChangesAsync();
            
            return true;
        }
    }
} 