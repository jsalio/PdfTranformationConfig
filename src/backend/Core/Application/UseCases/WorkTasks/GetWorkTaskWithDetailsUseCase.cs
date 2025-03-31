namespace Backend.Core.Application.UseCases.WorkTasks
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Backend.Core.Application.DTOs;
    using Backend.Core.Application.Interfaces.Repositories;
    using Backend.Core.Application.Interfaces.UseCases;
    
    public class GetWorkTaskWithDetailsUseCase : IUseCase<int, (WorkTaskDto? WorkTask, IEnumerable<WorkTaskDetailDto> Details)>
    {
        private readonly IWorkTaskRepository _workTaskRepository;
        private readonly IWorkTaskDetailRepository _workTaskDetailRepository;
        
        public GetWorkTaskWithDetailsUseCase(
            IWorkTaskRepository workTaskRepository,
            IWorkTaskDetailRepository workTaskDetailRepository)
        {
            _workTaskRepository = workTaskRepository;
            _workTaskDetailRepository = workTaskDetailRepository;
        }
        
        public async Task<(WorkTaskDto? WorkTask, IEnumerable<WorkTaskDetailDto> Details)> ExecuteAsync(int id)
        {
            var workTask = await _workTaskRepository.GetByIdAsync(id);
            if (workTask == null)
                return (null, Enumerable.Empty<WorkTaskDetailDto>());
                
            var workTaskDto = new WorkTaskDto
            {
                Id = workTask.Id,
                Name = workTask.Name,
                Created = workTask.Created,
                ConfigurationId = workTask.ConfigurationId,
                Status = workTask.Status
            };
            
            var details = await _workTaskDetailRepository.GetByWorkTaskIdAsync(id);
            var detailDtos = details.Select(d => new WorkTaskDetailDto
            {
                Id = d.Id,
                WorkTaskId = d.WorkTaskId,
                DocumentId = d.DocumentId,
                Status = d.Status,
                Created = d.Created,
                LastDateWork = d.LastDateWork
            });
            
            return (workTaskDto, detailDtos);
        }
    }
} 