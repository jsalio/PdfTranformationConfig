using PdfConverter.Core.Application.Interfaces.Repositories;
using PdfConverter.Core.Domain.Entities;

namespace Backend.Core.Application.UseCases.Configurations
{
    using System.Threading.Tasks;
    using Backend.Core.Application.DTOs;
    using Backend.Core.Application.Interfaces.Repositories;
    using Backend.Core.Application.Interfaces.UseCases;
    using Backend.Core.Domain.Entities;
    
    public class CreateConfigurationUseCase : IUseCase<ConfigurationDto, ConfigurationDto>
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IEngineRepository _engineRepository;
        
        public CreateConfigurationUseCase(
            IConfigurationRepository configurationRepository,
            IEngineRepository engineRepository)
        {
            _configurationRepository = configurationRepository;
            _engineRepository = engineRepository;
        }
        
        public async Task<ConfigurationDto> ExecuteAsync(ConfigurationDto request)
        {
            // Validar que el motor exista
            var engine = await _engineRepository.GetByIdAsync(request.EngineId);
            if (engine == null)
                throw new KeyNotFoundException($"Engine with ID {request.EngineId} not found");
                
            // Crear la configuración
            var configuration = new Configuration
            {
                Name = request.Name,
                Description = request.Description,
                WorkflowId = request.WorkflowId,
                EngineId = request.EngineId,
                RetryLimit = request.RetryLimit
            };
            
            await _configurationRepository.AddAsync(configuration);
            await _configurationRepository.SaveChangesAsync();
            
            request.Id = configuration.Id;
            return request;
        }
    }
} 