using PdfConverter.Core.Application.Interfaces.Repositories;

namespace Backend.Core.Application.UseCases.Configurations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Backend.Core.Application.DTOs;
    using Backend.Core.Application.Interfaces.Repositories;
    using Backend.Core.Application.Interfaces.UseCases;
    
    public class GetAllConfigurationsUseCase : IUseCase<object, IEnumerable<ConfigurationDto>>
    {
        private readonly IConfigurationRepository _configurationRepository;
        
        public GetAllConfigurationsUseCase(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }
        
        public async Task<IEnumerable<ConfigurationDto>> ExecuteAsync(object _)
        {
            var configurations = await _configurationRepository.GetAllAsync();
            
            return configurations.Select(c => new ConfigurationDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                WorkflowId = c.WorkflowId,
                EngineId = c.EngineId,
                RetryLimit = c.RetryLimit
            });
        }
    }
} 