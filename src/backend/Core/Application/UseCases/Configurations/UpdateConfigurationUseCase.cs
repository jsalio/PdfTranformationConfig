using PdfConverter.Core.Application.Interfaces.Repositories;

namespace Backend.Core.Application.UseCases.Configurations
{
    using System.Threading.Tasks;
    using Backend.Core.Application.DTOs;
    using Backend.Core.Application.Interfaces.Repositories;
    using Backend.Core.Application.Interfaces.UseCases;
    
    public class UpdateConfigurationUseCase : IUseCase<ConfigurationDto, ConfigurationDto?>
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IEngineRepository _engineRepository;
        
        public UpdateConfigurationUseCase(
            IConfigurationRepository configurationRepository,
            IEngineRepository engineRepository)
        {
            _configurationRepository = configurationRepository;
            _engineRepository = engineRepository;
        }
        
        public async Task<ConfigurationDto?> ExecuteAsync(ConfigurationDto request)
        {
            var configuration = await _configurationRepository.GetByIdAsync(request.Id);
            if (configuration == null)
                return null;
                
            // Validar que el motor exista
            var engine = await _engineRepository.GetByIdAsync(request.EngineId);
            if (engine == null)
                throw new KeyNotFoundException($"Engine with ID {request.EngineId} not found");
                
            // Actualizar la configuración
            configuration.Name = request.Name;
            configuration.Description = request.Description;
            configuration.WorkflowId = request.WorkflowId;
            configuration.EngineId = request.EngineId;
            configuration.RetryLimit = request.RetryLimit;
            
            await _configurationRepository.UpdateAsync(configuration);
            await _configurationRepository.SaveChangesAsync();
            
            return request;
        }
    }
} 