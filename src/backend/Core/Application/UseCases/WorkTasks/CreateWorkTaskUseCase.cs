using PdfConverter.Core.Application.Interfaces.Repositories;

namespace Backend.Core.Application.UseCases.WorkTasks
{
    using System;
    using System.Threading.Tasks;
    using Backend.Core.Application.DTOs;
    using Backend.Core.Application.Interfaces.Repositories;
    using Backend.Core.Application.Interfaces.UseCases;
    using Backend.Core.Domain.Entities;
    using Backend.Core.Domain.Enums;
    
    public class CreateWorkTaskUseCase : IUseCase<WorkTaskDto, WorkTaskDto>
    {
        private readonly IWorkTaskRepository _workTaskRepository;
        private readonly IConfigurationRepository _configurationRepository;
        
        public CreateWorkTaskUseCase(
            IWorkTaskRepository workTaskRepository,
            IConfigurationRepository configurationRepository)
        {
            _workTaskRepository = workTaskRepository;
            _configurationRepository = configurationRepository;
        }
        
        public async Task<WorkTaskDto> ExecuteAsync(WorkTaskDto request)
        {
            // Validar que la configuración exista
            var configuration = await _configurationRepository.GetByIdAsync(request.ConfigurationId);
            if (configuration == null)
                throw new KeyNotFoundException($"Configuration with ID {request.ConfigurationId} not found");
                
            // Crear la tarea de trabajo
            var workTask = new WorkTask
            {
                Name = request.Name,
                Created = DateTime.UtcNow,
                ConfigurationId = request.ConfigurationId,
                Status = WorkTaskStatus.Pending
            };
            
            await _workTaskRepository.AddAsync(workTask);
            await _workTaskRepository.SaveChangesAsync();
            
            request.Id = workTask.Id;
            request.Created = workTask.Created;
            request.Status = workTask.Status;
            
            return request;
        }
    }
} 