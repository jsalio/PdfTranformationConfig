using PdfConverter.Core.Application.Interfaces.Repositories;

namespace Backend.Core.Application.UseCases.Configurations
{
    using System.Threading.Tasks;
    using Backend.Core.Application.DTOs;
    using Backend.Core.Application.Interfaces.Repositories;
    using Backend.Core.Application.Interfaces.UseCases;
    
    public class GetConfigurationByIdUseCase : IUseCase<int, ConfigurationDto?>
    {
        private readonly IConfigurationRepository _configurationRepository;
        
        public GetConfigurationByIdUseCase(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }
        
        public async Task<ConfigurationDto?> ExecuteAsync(int id)
        {
            var configuration = await _configurationRepository.GetByIdAsync(id);
            
            if (configuration == null)
                return null;
                
            return new ConfigurationDto
            {
                Id = configuration.Id,
                Name = configuration.Name,
                Description = configuration.Description,
                WorkflowId = configuration.WorkflowId,
                EngineId = configuration.EngineId,
                RetryLimit = configuration.RetryLimit
            };
        }
    }
} 